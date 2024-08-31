using IngredientAnalyzer.AzureFunction.Interfaces;
using IngredientAnalyzer.AzureFunction.MessageHandler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace IngredientAnalyzer.AzureFunction.ServiceConfiguration.MessageHandler;

public static class Extensions{
    public static IServiceCollection AddTelegramMessageHandler(this IServiceCollection services, HostBuilderContext context)
    {
        var telegramBotToken = context.Configuration["TelegramBotToken"] 
            ?? throw new InvalidOperationException("TelegramBotToken config value is missing");

        services.AddHttpClient("TelegramBotClient")
            .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(telegramBotToken, httpClient));
        services.AddSingleton<IMessageHandler, TelegramMessageHandler>();
        return services;
    }
}