using System.Text.Json;

namespace StrongTypedId.Converters;

internal class
	StrongTypedIdJsonConverter<TStrongTypedId, TPrimitiveId> : StrongTypedValueJsonConverter<TStrongTypedId,
	TPrimitiveId>
	where TStrongTypedId : IStrongTypedId<TStrongTypedId, TPrimitiveId>
	where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
	IParsable<TPrimitiveId>
{
	public override TStrongTypedId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert,
		JsonSerializerOptions options)
	{
		var value = reader.GetString();
		if (value is null)
		{
			return default!;
		}

		return IStrongTypedId<TStrongTypedId, TPrimitiveId>.Parse(value);
	}

	public override void WriteAsPropertyName(Utf8JsonWriter writer, TStrongTypedId value, JsonSerializerOptions options)
	{
		writer.WritePropertyName(value.ToString()!);
	}
}