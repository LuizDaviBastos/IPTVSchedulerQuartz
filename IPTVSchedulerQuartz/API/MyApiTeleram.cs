using IPTVSchedulerQuartz.lib.ExtensionMethod;
using IPTVSchedulerQuartz.ModeSettings;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace IPTVSchedulerQuartz.API
{
    public class MyApiTelegram
    {
        public TelegramBotClient botClient;
        private ApiTelegramSettings _settings;
        private IConfiguration _configuration;

        public MyApiTelegram(IConfiguration configuration)
        {
            _configuration = configuration;
            this._settings = GetConfig<ApiTelegramSettings>.Get(_configuration).Result;
            this.botClient = new TelegramBotClient(this._settings.Token);

            this.botClient.StartReceiving();
        }

        public Task<Message> EnviarMensagem(string message)
        {
            return this.botClient.SendTextMessageAsync(new ChatId(920002748), message);//My ChatId 920002748
        }

    }
}
