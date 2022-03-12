using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongTypedId.Converters
{
	/// <Summary>
	/// JsonConverter for System.Text.Json. It serializes purely the underlying value. Use it like this:
	/// [JsonConverter(typeof(SystemTextJsonConverter<UserId, Guid>))]
	/// public class UserId: StrongTypedId<UserId, Guid>
	/// </Summary>
	public class SystemTextJsonConverter<TStrongTypedId, TPrimitiveId> : JsonConverter<TStrongTypedId>
		where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>
	{
		public override TStrongTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = (TPrimitiveId)GetValue(reader);
			return StrongTypedId<TStrongTypedId, TPrimitiveId>.Create(value);
		}

		private static object GetValue(Utf8JsonReader reader)
		{
			return typeof(TPrimitiveId) switch
			{
				Type t when t == typeof(bool) => reader.GetBoolean(),
				Type t when t == typeof(Guid) => reader.GetGuid(),
				Type t when t == typeof(short) => reader.GetInt16(),
				Type t when t == typeof(int) => reader.GetInt32(),
				Type t when t == typeof(long) => reader.GetInt64(),
				Type t when t == typeof(ushort) => reader.GetUInt16(),
				Type t when t == typeof(uint) => reader.GetUInt32(),
				Type t when t == typeof(ulong) => reader.GetUInt64(),
				Type t when t == typeof(float) => reader.GetSingle(),
				Type t when t == typeof(double) => reader.GetDouble(),
				Type t when t == typeof(decimal) => reader.GetDecimal(),
				Type t when t == typeof(byte) => reader.GetByte(),
				Type t when t == typeof(sbyte) => reader.GetSByte(),
				_ => throw new NotSupportedException()
			};
		}

		public override void Write(Utf8JsonWriter writer, TStrongTypedId value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}

}
