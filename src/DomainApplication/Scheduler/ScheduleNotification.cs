using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DomainApplication.Scheduler
{
    public class ScheduleNotification : ScheduledProcessor
    {
        public ScheduleNotification(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "* * * * *";

        protected override async Task ProcessInScopeAsync(IServiceProvider serviceProvider)
        {
            //var notification = serviceProvider.GetService<INotificationService>();
            //await notification.BackgroundNotificationSend();
            //var toDoTaskService = serviceProvider.GetService<IToDoTaskService>();
            //await toDoTaskService.BackgroundTaskNotificationAsync();
            //return Task.CompletedTask;
        }
    }
}