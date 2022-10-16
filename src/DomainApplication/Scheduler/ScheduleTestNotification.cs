using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DomainApplication.Scheduler
{
    public class ScheduleTestNotification : ScheduledProcessor
    {
        public ScheduleTestNotification(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "0 0 * * *";

        protected override async Task ProcessInScopeAsync(IServiceProvider serviceProvider)
        {
            //IPenetrationTestItemService notification = serviceProvider.GetService<IPenetrationTestItemService>();
            //await notification.BackgroundTestItemNotificationAsync();
        }
    }
}