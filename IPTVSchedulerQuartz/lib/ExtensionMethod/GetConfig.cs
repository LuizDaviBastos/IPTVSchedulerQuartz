using Microsoft.Extensions.Configuration;
using System;

namespace IPTVSchedulerQuartz.lib.ExtensionMethod
{
    public class GetConfig<TModel>
    {
        
        public static ResponseConfig Get(IConfiguration config)
        {
            try
            {
                var obj = config.GetSection(typeof(TModel).Name).Get<TModel>();
                return new ResponseConfig()
                {
                    Result = obj,
                    IsConfigured = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseConfig()
                {
                    IsConfigured = false
                };
            }
        }

        public class ResponseConfig : IResponseConfig
        {
            public TModel Result { get; set; }
            public bool IsConfigured { get; set; }
        }

        public interface IResponseConfig
        {
            TModel Result { get; set; }
            bool IsConfigured { get; set; }
        }

    }
}
