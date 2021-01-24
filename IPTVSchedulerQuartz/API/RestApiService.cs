using IPTVSchedulerQuartz.lib.ExtensionMethod;
using IPTVSchedulerQuartz.Models;
using IPTVSchedulerQuartz.ModeSettings;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace IPTVSchedulerQuartz.API
{
    public class RestApiService
    {
        private readonly MyApiTelegram _myApiTelegram;
        private readonly IptvApiSettings _iptvApiSettings;

        public RestApiService(IConfiguration configuration, MyApiTelegram myApiTelegram)
        {
            this._myApiTelegram = myApiTelegram;
            this._iptvApiSettings = GetConfig<IptvApiSettings>.Get(configuration).Result;
        }

        public async Task PostDay(ListIptvItem listIptvItem)
        {
            //await this._myApiTelegram.EnviarMensagem($"Você tem uma nova notificação de {today.ToLongDateString()}");
            await this._myApiTelegram
                .EnviarMensagem($"Lista de {listIptvItem.Reseller_Username} expira em {new DateTime(listIptvItem.Exp_Date ?? 0).ToLongDateString()}");
        }

        public async Task<ListIptv> GetListIPTV()
        {
            var client = new RestClient(this._iptvApiSettings.BaseUrl);

            var request = new RestRequest(this._iptvApiSettings.ResourceGetList, Method.GET)
                .AddHeader("authorization", this._iptvApiSettings.AuthorizationBearer)
                .AddHeader("x-xsrf-token", this._iptvApiSettings.XsrfToken)
                .AddParameter("from", "null", ParameterType.QueryString)
                .AddParameter("p", 1).AddParameter("r", 0)
                .AddParameter("s", "null").AddParameter("sort-by", "id")
                .AddParameter("sort-desc", "true").AddParameter("sort-by", "id")
                .AddParameter("to", "null").AddParameter("type", 6);

            var requestResult = await client.ExecuteAsync(request);
            
            if (requestResult?.IsSuccessful ?? false)
            {
                var content = requestResult.Content;
                return JsonConvert.DeserializeObject<ListIptv>(content);
            }
            else
            {
                dynamic exceptionContent = requestResult.Content;
                if (exceptionContent?.type == 1 || (exceptionContent?.message != null && 
                    (exceptionContent?.message as string).Contains("Desculpe, sua sessão expirou")))
                {
                    //resolve expirate
                    return null;
                }
                else
                {
                    //resolve some problem
                    return null;
                }

            }


        }
    }
}

