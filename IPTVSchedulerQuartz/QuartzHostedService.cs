using IPTVSchedulerQuartz.DTO;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IPTVSchedulerQuartz
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<DTOJobScheduler> _jobs;

        public IScheduler Scheduler { get; set; }

        public QuartzHostedService(IJobFactory jobFactory, ISchedulerFactory schedulerFactory, IEnumerable<DTOJobScheduler> jobs)
        {
            this._schedulerFactory = schedulerFactory;
            this._jobFactory = jobFactory;
            this._jobs = jobs;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var job in _jobs)
            {
                var currentJob = CreateJob(job);
                var trigger = CreateTrigger(job);

                await Scheduler.ScheduleJob(currentJob, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        private static IJobDetail CreateJob(DTOJobScheduler schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private static ITrigger CreateTrigger(DTOJobScheduler schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }
}
