using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KdAtrio.Converters
{
    public class DateOnlyJsonConverter : JsonConverter<DateTime>
    {
        private const string Format = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            return DateTime.ParseExact(str!, Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
