using System.Globalization;
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
		return StrongTypedExtensions.Create<TStrongTypedValue, TPrimitiveValue>(value);
	}

	private static object GetValue(Utf8JsonReader reader)
	{
		var primitiveType = typeof(TPrimitiveValue);
		if (reader.TokenType == JsonTokenType.String && primitiveType != typeof(string) && primitiveType != typeof(char) &&
		    primitiveType != typeof(DateTime)) // Attempt to Parse instead, if applicable
		{
			return ParseValue(reader, primitiveType);
		}

		return primitiveType switch
		{
			not null when primitiveType == typeof(bool) => reader.GetBoolean(),
			not null when primitiveType == typeof(char) => reader.GetString() is { Length: 1 } s
				? s[0]
				: throw new JsonException($"Expected single-character string for char at index {reader.TokenStartIndex}"),
			not null when primitiveType == typeof(Guid) => reader.GetGuid(),
			not null when primitiveType == typeof(short) => reader.GetInt16(),
			not null when primitiveType == typeof(int) => reader.GetInt32(),
			not null when primitiveType == typeof(long) => reader.GetInt64(),
			not null when primitiveType == typeof(ushort) => reader.GetUInt16(),
			not null when primitiveType == typeof(uint) => reader.GetUInt32(),
			not null when primitiveType == typeof(ulong) => reader.GetUInt64(),
			not null when primitiveType == typeof(float) => reader.GetSingle(),
			not null when primitiveType == typeof(double) => reader.GetDouble(),
			not null when primitiveType == typeof(decimal) => reader.GetDecimal(),
			not null when primitiveType == typeof(byte) => reader.GetByte(),
			not null when primitiveType == typeof(sbyte) => reader.GetSByte(),
			not null when primitiveType == typeof(string) => reader.GetString()!,
			not null when primitiveType == typeof(DateTime) => reader.GetDateTime(),
			_ => throw new NotSupportedException()
		};
	}

	private static object ParseValue(Utf8JsonReader reader, Type primitiveType)
	{
		var value = reader.GetString() ?? throw new JsonException($"Failed to deserialize null value to type {primitiveType.Name} at index {reader.TokenStartIndex}");
		return primitiveType switch
		{
			not null when primitiveType == typeof(bool) => bool.Parse(value),
			not null when primitiveType == typeof(Guid) => Guid.Parse(value),
			not null when primitiveType == typeof(short) => short.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(int) => int.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(long) => long.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(ushort) => ushort.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(uint) => uint.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(ulong) => ulong.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(float) => float.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(double) => double.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(decimal) => decimal.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(byte) => byte.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture),
			not null when primitiveType == typeof(sbyte) => sbyte.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture),
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