using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace IngredientAnalyzer.AzureFunction
{
    public class TelegramBotRunner(ITelegramBotClient telegramBotClient, ILoggerFactory loggerFactory)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<TelegramBotRunner>();
    

        [Function("TelegramBotRunner")]
        public async Task RunAsync([TimerTrigger("*/1 * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            var me = await telegramBotClient.GetMeAsync();

            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
        }
    }
}
