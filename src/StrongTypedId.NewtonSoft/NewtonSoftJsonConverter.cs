using System;
using Newtonsoft.Json;

namespace StrongTypedId.Converters
{
	/// <summary>
	/// JsonConverter for Newtonsoft. It serializes purely the underlying value. Use it like this:
	/// [JsonConverter(typeof(NewtonSoftJsonConverter&lt;UserId, Guid&gt;))]
	/// public class UserId: StrongTypedId&lt;UserId, Guid&gt;
	/// </summary>
	public class NewtonSoftJsonConverter<TStrongTypedValue, TPrimitiveValue> : JsonConverter<TStrongTypedValue>
		where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, TPrimitiveValue>
		where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>, IParsable<TPrimitiveValue>
	{
		public override TStrongTypedValue? ReadJson(JsonReader reader, Type objectType, TStrongTypedValue? existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			var result = serializer.Deserialize<TPrimitiveValue?>(reader);
			return result is not null
				? StrongTypedValue<TStrongTypedValue, TPrimitiveValue>.Create(result)
				: null;
		}

		public override void WriteJson(JsonWriter writer, TStrongTypedValue? value, Newtonsoft.Json.JsonSerializer serializer)
		{
			serializer.Serialize(writer, value is null ? null : value.PrimitiveId);
		}
	}
}