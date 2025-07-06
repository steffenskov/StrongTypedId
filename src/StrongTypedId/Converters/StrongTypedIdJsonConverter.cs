using System.Text.Json;

namespace StrongTypedId.Converters;

internal class
	StrongTypedIdJsonConverter<TStrongTypedId, TPrimitiveId> : StrongTypedValueJsonConverter<TStrongTypedId,
	TPrimitiveId>
	where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
	where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
	IParsable<TPrimitiveId>
{
	public override TStrongTypedId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert,
		JsonSerializerOptions options)
	{
		var value = reader.GetString();
		if (value is null)
		{
			return null!;
		}

		return StrongTypedId<TStrongTypedId, TPrimitiveId>.Parse(value);
	}

	public override void WriteAsPropertyName(Utf8JsonWriter writer, TStrongTypedId value, JsonSerializerOptions options)
	{
		writer.WritePropertyName(value.ToString());
	}
}