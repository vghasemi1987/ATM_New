using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Web.Extensions
{
    public sealed class AppViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            return viewLocations.Select(f => f.Replace("/Views/{1}", "/Core/{1}/Views/").Replace("/Views/Shared/", "/Core/Shared/"));
        }
    }
}