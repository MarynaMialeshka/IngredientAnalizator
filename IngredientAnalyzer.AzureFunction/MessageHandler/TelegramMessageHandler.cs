using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using IngredientAnalyzer.AzureFunction.Interfaces;

namespace IngredientAnalyzer.AzureFunction.MessageHandler;

public class TelegramMessageHandler(ITelegramBotClient telegramBotClient, ILogger<TelegramMessageHandler> logger) : IMessageHandler
{

    public async Task OnUpdateAsync(Update? update)
    {
        if (update is null || update.Message is not Message message) 
        {
            logger.LogInformation("Update message is null or cannot be handled.");
            return;
        }

        logger.LogInformation("Handling update message... ChatId : {chatId}", message.Chat.Id);

        //get message type and corresponding handler
        //handle update message
        
        await  telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"You said: {message.Text}");
    }
}