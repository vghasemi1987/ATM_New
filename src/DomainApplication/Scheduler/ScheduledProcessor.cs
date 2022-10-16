using System;
using System.Threading;
using System.Threading.Tasks;
using DomainApplication.BackgroundService;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;

namespace DomainApplication.Scheduler
{
    public abstract class ScheduledProcessor : ScopedProcessor
    {
        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;
        protected abstract string Schedule { get; }

        protected ScheduledProcessor(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}