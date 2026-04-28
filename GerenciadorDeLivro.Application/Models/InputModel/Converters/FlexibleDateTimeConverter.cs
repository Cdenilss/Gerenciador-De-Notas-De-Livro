using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GerenciadorDeLivro.Application.Models.InputModel.Converters;

public sealed class FlexibleDateTimeConverter : JsonConverter<DateTime>
{
    private static readonly string[] AcceptedFormats =
    {
        "dd/MM/yyyy",
        "dd-MM-yyyy",
        "yyyy-MM-dd",
        "yyyy-MM-ddTHH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss.FFFFFFF",
        "yyyy-MM-ddTHH:mm:ssK",
        "yyyy-MM-ddTHH:mm:ss.FFFFFFFK",
        "O"
    };

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("A data deve ser enviada como texto.");
        }

        var value = reader.GetString();

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new JsonException("A data não pode ser vazia.");
        }

        if (DateTime.TryParseExact(value, AcceptedFormats, CultureInfo.GetCultureInfo("pt-BR"),
                DateTimeStyles.AssumeLocal, out var result) ||
            DateTime.TryParseExact(value, AcceptedFormats, CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal, out result))
        {
            return result;
        }

        throw new JsonException("Formato de data inválido. Use dd/MM/yyyy ou dd-MM-yyyy.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}
