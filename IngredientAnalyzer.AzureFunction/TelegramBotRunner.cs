using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using IngredientAnalyzer.AzureFunction.Interfaces;
using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json;
using Telegram.Bot.Types;

namespace IngredientAnalyzer.AzureFunction
{
    public class TelegramBotRunner(IMessageHandler messageHandler, ILogger<TelegramBotRunner> logger)
    {

        [Function("TelegramBotRunner")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData request)
        {
            logger.LogInformation("HTTP trigger function executing...");
        
            var response = request.CreateResponse(System.Net.HttpStatusCode.OK);

            try
            {
                var requestBody = await request.ReadAsStringAsync() ?? throw new ArgumentNullException(nameof(request));
                var update = JsonSerializer.Deserialize<Update>(requestBody);
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
