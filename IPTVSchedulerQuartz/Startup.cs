using IPTVSchedulerQuartz.API;
using IPTVSchedulerQuartz.DTO;
using IPTVSchedulerQuartz.JobFactory;
using IPTVSchedulerQuartz.Jobs;
using IPTVSchedulerQuartz.lib.ExtensionMethod;
using IPTVSchedulerQuartz.ModeSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace IPTVSchedulerQuartz
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly IConfiguration Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //background service
            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<SendRequestJob>();

            var config = GetConfig<BgHostedConfig>.Get(Configuration);

            services.AddSingleton(
                new DTOJobScheduler(
                    jobType: typeof(SendRequestJob),
                    //cronExpression: (config.IsConfigured ? (config?.Result?.CronExpression ?? "1 1 1 * * ?") : "1 1 1 * * ?") //todos os dias ás 1:00am - "second, minute, hour"
                    //cronExpression: "0/50 * * * * ?"
                    cronExpression: "1 1 1 * * ?"
                ));
            services.AddScoped<RestApiService>();
            services.AddScoped<MyApiTelegram>();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
