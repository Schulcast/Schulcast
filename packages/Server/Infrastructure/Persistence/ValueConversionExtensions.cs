using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Schulcast.Infrastructure.Persistence;

public static class ValueConversionExtensions
{
	public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder, T defaultValue = default!)
	{
		var jsonSerializerOptions = new JsonSerializerOptions();

		var converter = new ValueConverter<T, string>(
			v => JsonSerializer.Serialize(v, jsonSerializerOptions),
			v => JsonSerializer.Deserialize<T>(v, jsonSerializerOptions) ?? defaultValue
		);

		var comparer = new ValueComparer<T>(
			(l, r) => JsonSerializer.Serialize(l, jsonSerializerOptions) == JsonSerializer.Serialize(r, jsonSerializerOptions),
			v => v == null ? 0 : JsonSerializer.Serialize(v, jsonSerializerOptions).GetHashCode(),
			v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, jsonSerializerOptions), jsonSerializerOptions) ?? defaultValue
		);

		propertyBuilder.HasConversion(converter);
		propertyBuilder.Metadata.SetValueConverter(converter);
		propertyBuilder.Metadata.SetValueComparer(comparer);

		return propertyBuilder;
	}
}