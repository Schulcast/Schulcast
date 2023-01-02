namespace Schulcast.Application.Content;

public static class XmlExtensions
{
	public static string XmlSerializeToString(this object objectInstance)
	{
		var serializer = new XmlSerializer(objectInstance.GetType());
		var sb = new StringBuilder();

		using var writer = new StringWriter(sb);
		serializer.Serialize(writer, objectInstance);

		return sb.ToString();
	}

	public static T XmlDeserializeFromString<T>(this string objectData)
	{
		return (T)XmlDeserializeFromString(objectData, typeof(T));
	}

	public static object XmlDeserializeFromString(this string objectData, Type type)
	{
		var serializer = new XmlSerializer(type);
		using var reader = new StringReader(objectData);
		var result = serializer.Deserialize(reader)!;
		return result;
	}
}