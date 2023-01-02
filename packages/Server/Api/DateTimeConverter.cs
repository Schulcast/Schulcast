namespace Schulcast.Api;

public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
{
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.GetDateTime().ToUniversalTime();

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToUniversalTime());
}