using System;
using Newtonsoft.Json;

namespace StrongTypedId.Converters
{
	/// <summary>
	/// JsonConverter for Newtonsoft. It serializes purely the underlying value. Use it like this:
	/// [JsonConverter(typeof(NewtonSoftJsonConverter<UserId, Guid>))]
	/// public class UserId: StrongTypedId<UserId, Guid>
	/// </summary>
	public class NewtonSoftJsonConverter<TStrongTypedId, TPrimitiveId> : JsonConverter<TStrongTypedId>
		where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>, IParsable<TPrimitiveId>
	{
		public override TStrongTypedId? ReadJson(JsonReader reader, Type objectType, TStrongTypedId? existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			var result = serializer.Deserialize<TPrimitiveId?>(reader);
			return result.HasValue
				? StrongTypedId<TStrongTypedId, TPrimitiveId>.Create(result.Value)
				: null;
		}

		public override void WriteJson(JsonWriter writer, TStrongTypedId? value, Newtonsoft.Json.JsonSerializer serializer)
		{
			serializer.Serialize(writer, value?.PrimitiveId);
		}
	}
}