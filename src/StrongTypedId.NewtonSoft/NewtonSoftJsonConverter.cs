using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StrongTypedId.Converters;

/// <summary>
///     JsonConverter for Newtonsoft. It serializes purely the underlying value. Use it like this:
///     [JsonConverter(typeof(NewtonSoftJsonConverter&lt;UserId, Guid&gt;))]
///     public class UserId: StrongTypedId&lt;UserId, Guid&gt;
/// </summary>
[Obsolete("Consider using StrongTypedNewtonSoftJsonConverter instead, the attribute approach won't be maintained in the future.")]
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

		if (serializer.TypeNameHandling == TypeNameHandling.None)
		{
			var result = serializer.Deserialize<TPrimitiveValue>(reader);
			return result is not null
				? StrongTypedValue<TStrongTypedValue, TPrimitiveValue>.Create(result)
				: null;
		}

		var bypassSerializer = CreateBypassSerializer(serializer);
		bypassSerializer.TypeNameHandling = TypeNameHandling.All;
		return bypassSerializer.Deserialize<TStrongTypedValue>(reader);
	}

	public override void WriteJson(JsonWriter writer, TStrongTypedValue? value, JsonSerializer serializer)
	{
		if (value is null)
		{
			writer.WriteNull();
			return;
		}

		if (serializer.TypeNameHandling == TypeNameHandling.None)
		{
			serializer.Serialize(writer, value.PrimitiveValue);
		}
		else
		{
			var bypassSerializer = CreateBypassSerializer(serializer);
			bypassSerializer.TypeNameHandling = TypeNameHandling.All;
			bypassSerializer.Serialize(writer, value);
		}
	}

	private static JsonSerializer CreateBypassSerializer(JsonSerializer originalSerializer)
	{
		var result = new JsonSerializer
		{
			TypeNameHandling = TypeNameHandling.All,
			ContractResolver = new BypassResolver()
		};
		return result;
	}

	private class BypassResolver : DefaultContractResolver
	{
		public override JsonContract ResolveContract(Type type)
		{
			var contract = base.ResolveContract(type);
			contract.Converter = null;
			return contract;
		}
	}
}