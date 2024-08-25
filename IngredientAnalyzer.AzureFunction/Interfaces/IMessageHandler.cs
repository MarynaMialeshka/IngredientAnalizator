using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IngredientAnalyzer.AzureFunction.Interfaces;

public interface IMessageHandler{
    Task OnMessage(Message msg, UpdateType type);
}