using Autofac;
using DomainApplication.Scheduler;
using DomainContracts.Commons;
using Infrastructure.Data.Commons;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using Web.Extensions.Attributes;

namespace Web.Extensions.AutofacServiceResolver
{
    public class WebHandlerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IAsyncRepository<>));

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(Infrastructure)), Assembly.Load(nameof(DomainApplication)))
           .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service"))
           .AsImplementedInterfaces();
           //.Except<typeof(EfRepository<>)>(typeof(EfRepository<>));
            
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerDependency();
            builder.RegisterType<SmsSender>().As<ISmsSender>().InstancePerDependency();

            builder.RegisterType<UserActivityLogAttribute>().SingleInstance();
            //builder.RegisterType<UserResolverService>().InstancePerDependency();


            builder.RegisterType<ScheduleNotification>().As<IHostedService>().SingleInstance();
            builder.RegisterType<ScheduleTestNotification>().As<IHostedService>().SingleInstance();
            builder.RegisterType<ControllerDiscovery.ControllerDiscovery>().SingleInstance();
            builder.RegisterType<PermissionAuthorizationHandler>().As<IAuthorizationHandler>().SingleInstance();
        }
    }
}
