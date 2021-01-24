using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTVSchedulerQuartz.lib.ExtensionMethod
{
    public static class GetConfigTwo
    {
        public static TConfigModel GetConfig<TConfigModel>(this IConfiguration config)
        {
            return config.GetSection(typeof(TConfigModel).Name).Get<TConfigModel>();
        }
    }
}
