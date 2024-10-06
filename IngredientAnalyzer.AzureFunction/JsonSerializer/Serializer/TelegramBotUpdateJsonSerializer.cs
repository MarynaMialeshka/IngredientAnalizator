using System.Text.Json;
using System.Text.Json.Serialization;
using IngredientAnalyzer.AzureFunction.JsonSerializer.Converter;

namespace IngredientAnalyzer.AzureFunction.JsonSerializer.Serializer;

public class TelegramBotUpdateJsonSerializer : BaseJsonSerializer<TelegramBotRunner>
{
    protected override void ConfigureJsonSerializerOptions()
    {
        JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        JsonSerializerOptions.Converters.Add(new UnixDateTimeConverter());
    }
}