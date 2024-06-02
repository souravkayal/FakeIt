using System.Text.Json;
using System.Text.Json.Serialization;

namespace FakeIt.Common.Common
{
    public class RemoveBackslashesConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            // Serialize and remove backslashes
            var cleanedValue = value.Replace("\\", string.Empty);
            writer.WriteStringValue(cleanedValue);
        }
    }
}
