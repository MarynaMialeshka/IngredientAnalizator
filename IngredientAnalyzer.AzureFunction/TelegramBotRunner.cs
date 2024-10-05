using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using IngredientAnalyzer.AzureFunction.Interfaces;
using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json;
using Telegram.Bot.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IngredientAnalyzer.AzureFunction
{
    public class TelegramBotRunner(IMessageHandler messageHandler, IConfiguration configuration, 
        IOptions<JsonSerializerOptions> jsonSerializerOptions, ILogger<TelegramBotRunner> logger)
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
                var update = System.Text.Json.JsonSerializer.Deserialize<Update>(requestBody, jsonSerializerOptions.Value);
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
