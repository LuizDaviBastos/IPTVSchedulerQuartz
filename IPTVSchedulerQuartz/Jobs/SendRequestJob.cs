using IPTVSchedulerQuartz.API;
using IPTVSchedulerQuartz.lib.ExtensionMethod;
using IPTVSchedulerQuartz.ModeSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace IPTVSchedulerQuartz.Jobs
{
    [DisallowConcurrentExecution]
    public class SendRequestJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public SendRequestJob(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this._serviceProvider = serviceProvider;
            this._configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var config = GetConfig<BgHostedConfig>.Get(this._configuration);
            if (config.IsConfigured && config.Result.Actived)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<RestApiService>();
                    await service.PostDay(DateTime.Now);
                    var result = await service.GetListIPTV();
                }
            }
            
            await Task.CompletedTask;
        }
    }
}
