using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using IngredientAnalyzer.AzureFunction.Interfaces;

namespace IngredientAnalyzer.AzureFunction
{
    public class TelegramBotRunner(IMessageHandler messageHandler, IConfiguration configuration, 
        IJsonSerializer<TelegramBotRunner> jsonSerializer, ILogger<TelegramBotRunner> logger)
    {
        private const string WebHookTokenHeaderName = "X-Telegram-Bot-Api-Secret-Token";

        [Function("TelegramBotRunner")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData request)
        {
            logger.LogInformation("HTTP trigger function executing...");
        
            var response = request.CreateResponse(System.Net.HttpStatusCode.OK);

            try
            {
                var webHookTokenHeaderValue = request.Headers.GetValues(WebHookTokenHeaderName)?.FirstOrDefault();
                if (string.IsNullOrEmpty(webHookTokenHeaderValue) 
                    || webHookTokenHeaderValue != configuration.GetValue<string>(WebHookTokenHeaderName))
                {
                    throw new UnauthorizedAccessException("Invalid WebHook secret token");
                }
                
                var requestBody = await request.ReadAsStringAsync() ?? throw new ArgumentNullException(nameof(request));

                var update = jsonSerializer.Deserialize<Update>(requestBody);
                if (update == null)
                {
                    logger.LogInformation("Unable to deserialize Update message");
                }

                await messageHandler.OnUpdateAsync(update);
            }
            catch(Exception e)
            {
                logger.LogError(e, "Error occurred while executing HTTP trigger function : {message}", e.Message);
            }

            return  response;
        }
    }
}
