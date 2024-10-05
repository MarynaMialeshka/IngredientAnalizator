using System.Text.Json;
using System.Text.Json.Serialization;

namespace IngredientAnalyzer.AzureFunction.JsonSerializer.Converter;

internal class UnixDateTimeConverter : JsonConverter<DateTime>
{
    private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options){
        var seconds = reader.GetInt64();
        if (seconds > 0)
            return UnixEpoch.AddSeconds(seconds);
        if (seconds == 0)
            return default;
        throw new JsonException($"Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {typeToConvert:CultureInfo.InvariantCulture}.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options){
        if (value == default)
            writer.WriteNumberValue(0L);
        else
        {
            long seconds = (long)(value.ToUniversalTime() - UnixEpoch).TotalSeconds;
            if (seconds >= 0)
                writer.WriteNumberValue(seconds);
            else
                throw new JsonException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
        }
    }
}