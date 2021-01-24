using System;

namespace IPTVSchedulerQuartz.DTO
{
    public class DTOJobScheduler
    {
        public Type JobType { get; set; }
        public string CronExpression { get; set; }

        public DTOJobScheduler(Type jobType, string cronExpression)
        {
            this.JobType = jobType;
            this.CronExpression = cronExpression;
        }
    }
}
