using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongTypedId.Converters;

internal class StrongTypedValueJsonConverter<TStrongTypedValue, TPrimitiveValue> : JsonConverter<TStrongTypedValue>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, TPrimitiveValue>
	where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
	public override TStrongTypedValue Read(ref Utf8JsonReader reader, Type typeToConvert,
		JsonSerializerOptions options)
	{
		var value = (TPrimitiveValue)GetValue(reader);
		return StrongTypedValue<TStrongTypedValue, TPrimitiveValue>.Create(value);
	}

	private static object GetValue(Utf8JsonReader reader)
	{
		var primitiveType = typeof(TPrimitiveValue);
		if (reader.TokenType == JsonTokenType.String && primitiveType != typeof(string) && primitiveType != typeof(char)) // Attempt to Parse instead, if applicable
		{
			var value = reader.GetString() ?? throw new JsonException($"Failed to deserialize null value to type {primitiveType.Name} at index {reader.TokenStartIndex}");
			return primitiveType switch
			{
				{ } t when t == typeof(bool) => bool.Parse(value),
				{ } t when t == typeof(Guid) => Guid.Parse(value),
				{ } t when t == typeof(short) => short.Parse(value),
				{ } t when t == typeof(int) => int.Parse(value),
				{ } t when t == typeof(long) => long.Parse(value),
				{ } t when t == typeof(ushort) => ushort.Parse(value),
				{ } t when t == typeof(uint) => uint.Parse(value),
				{ } t when t == typeof(ulong) => ulong.Parse(value),
				{ } t when t == typeof(float) => float.Parse(value),
				{ } t when t == typeof(double) => double.Parse(value),
				{ } t when t == typeof(decimal) => decimal.Parse(value),
				{ } t when t == typeof(byte) => byte.Parse(value),
				{ } t when t == typeof(sbyte) => sbyte.Parse(value),
				{ } t when t == typeof(DateTime) => DateTime.Parse(value),
				_ => throw new NotSupportedException()
			};
		}

		return primitiveType switch
		{
			{ } t when t == typeof(bool) => reader.GetBoolean(),
			{ } t when t == typeof(char) => reader.GetString()![0],
			{ } t when t == typeof(Guid) => reader.GetGuid(),
			{ } t when t == typeof(short) => reader.GetInt16(),
			{ } t when t == typeof(int) => reader.GetInt32(),
			{ } t when t == typeof(long) => reader.GetInt64(),
			{ } t when t == typeof(ushort) => reader.GetUInt16(),
			{ } t when t == typeof(uint) => reader.GetUInt32(),
			{ } t when t == typeof(ulong) => reader.GetUInt64(),
			{ } t when t == typeof(float) => reader.GetSingle(),
			{ } t when t == typeof(double) => reader.GetDouble(),
			{ } t when t == typeof(decimal) => reader.GetDecimal(),
			{ } t when t == typeof(byte) => reader.GetByte(),
			{ } t when t == typeof(sbyte) => reader.GetSByte(),
			{ } t when t == typeof(string) => reader.GetString()!,
			{ } t when t == typeof(DateTime) => reader.GetDateTime(),
			_ => throw new NotSupportedException()
		};
	}

	public override void Write(Utf8JsonWriter writer, TStrongTypedValue value, JsonSerializerOptions options)
	{
		var writeAction = GetWriteAction(writer, value);
		writeAction();
	}

	private static Action GetWriteAction(Utf8JsonWriter writer, TStrongTypedValue value)
	{
		return value.PrimitiveValue switch
		{
			bool val => () => writer.WriteBooleanValue(val),
			char val => () => writer.WriteStringValue(val.ToString()),
			Guid val => () => writer.WriteStringValue(val.ToString()),
			short val => () => writer.WriteNumberValue(val),
			int val => () => writer.WriteNumberValue(val),
			long val => () => writer.WriteNumberValue(val),
			ushort val => () => writer.WriteNumberValue(val),
			uint val => () => writer.WriteNumberValue(val),
			ulong val => () => writer.WriteNumberValue(val),
			float val => () => writer.WriteNumberValue(val),
			double val => () => writer.WriteNumberValue(val),
			decimal val => () => writer.WriteNumberValue(val),
			byte val => () => writer.WriteNumberValue(val),
			sbyte val => () => writer.WriteNumberValue(val),
			string val => () => writer.WriteStringValue(val),
			DateTime val => () => writer.WriteStringValue(val),
			_ => throw new NotSupportedException()
		};
	}
}