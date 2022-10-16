using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Extensions.Attributes;

namespace Web.Extensions.ControllerDiscovery
{
    public class ControllerDiscovery
    {
        private List<ControllerInfo> _controllers;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public ControllerDiscovery(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IEnumerable<ControllerInfo> GetControllers()
        {
            if (_controllers != null)
                return _controllers;

            _controllers = new List<ControllerInfo>();
            var items = _actionDescriptorCollectionProvider
                .ActionDescriptors.Items
                .Where(descriptor => descriptor.GetType() == typeof(ControllerActionDescriptor))
                .Select(descriptor => (ControllerActionDescriptor)descriptor)
                .GroupBy(descriptor => descriptor.ControllerTypeInfo.FullName)
                .ToList();

            foreach (var actionDescriptors in items)
            {
                if (!actionDescriptors.Any())
                    continue;
                var actionDescriptor = actionDescriptors.First();
                var controllerTypeInfo = actionDescriptor.ControllerTypeInfo;

                var currentController = new ControllerInfo
                {
                    AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    DisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    Name = actionDescriptor.ControllerName
                };

                var actions = (from descriptor in actionDescriptors.GroupBy(a => a.ActionName).Select(g => g.First())
                               let methodInfo = descriptor.MethodInfo
                               where IsProtectedAction(controllerTypeInfo, methodInfo)
                               select new ActionInfo
                               {
                                   ControllerId = currentController.Id,
                                   Name = descriptor.ActionName,
                                   DisplayName = methodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName
                               })
                    .Distinct()
                    .ToList();

                if (!actions.Any()) continue;
                currentController.Actions = actions;
                _controllers.Add(currentController);
            }
            return _controllers;
        }

        private static bool IsProtectedAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            var result = false;
            if (actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) != null)
                result = false;
            if (controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>(true) != null)
                result = true;
            if (actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>(true) != null)
                result = true;
            if (actionMethodInfo.GetCustomAttribute<PermissionAttribute>(true) == null)
                result = false;
            if (actionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName == null)
                result = false;

            return result;
        }
    }

    public class ControllerInfo
    {
        public string Id => $"{Name}";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string AreaName { get; set; }

        public IEnumerable<ActionInfo> Actions { get; set; }
    }

    public class ActionInfo
    {
        public string Id => $"{ControllerId}_{Name}";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ControllerId { get; set; }
    }
}