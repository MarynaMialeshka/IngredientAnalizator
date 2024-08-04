using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace IngredientAnalyzer.AzureFunction.TelegramClient;

public static class Extensions{
    public static IServiceCollection AddTelegramClient(this IServiceCollection services, HostBuilderContext context)
    {
        var telegramBotToken = context.Configuration["TelegramBotToken"] 
            ?? throw new InvalidOperationException("TelegramBotToken config value is missing");
        
        return services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(telegramBotToken));
    }
}