using IngredientAnalyzer.AzureFunction.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IngredientAnalyzer.AzureFunction.MessageHandler;

public class TelegramMessageHandler : IMessageHandler
{
private readonly ILogger<TelegramMessageHandler> _logger;

    public TelegramMessageHandler(TelegramBotClient telegramBotClient, ILogger<TelegramMessageHandler> logger){
        
    }

    public async Task OnMessage(Message msg, UpdateType type){

    }
}