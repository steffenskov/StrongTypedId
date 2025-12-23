using Newtonsoft.Json;

namespace StrongTypedId.Converters;

/// <summary>
///     JsonConverter for Newtonsoft. It serializes purely the underlying value. Use it like this:
///     [JsonConverter(typeof(NewtonSoftJsonConverter&lt;UserId, Guid&gt;))]
///     public class UserId: StrongTypedId&lt;UserId, Guid&gt;
/// </summary>
public class NewtonSoftJsonConverter<TStrongTypedValue, TPrimitiveValue> : JsonConverter<TStrongTypedValue>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, TPrimitiveValue>
	where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
	public override TStrongTypedValue? ReadJson(JsonReader reader, Type objectType, TStrongTypedValue? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			return null;
		}

		var result = serializer.Deserialize<TPrimitiveValue>(reader);
		return result is not null
			? StrongTypedExtensions.Create<TStrongTypedValue, TPrimitiveValue>(result)
			: null;
	}

	public override void WriteJson(JsonWriter writer, TStrongTypedValue? value, JsonSerializer serializer)
	{
		serializer.Serialize(writer, value is null ? null : value.PrimitiveValue);
	}
}