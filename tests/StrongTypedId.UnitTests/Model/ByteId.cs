using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedByteId, byte>))]
[StrongTypedIdJsonConverter<AttributedByteId, byte>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedByteId, byte>))]
public class AttributedByteId : StrongTypedId<AttributedByteId, byte>
{
	public AttributedByteId(byte primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<ByteId, byte>))]
[StrongTypedIdJsonConverter<ByteId, byte>]
public class ByteId : StrongTypedId<ByteId, byte>
{
	public ByteId(byte primitiveValue) : base(primitiveValue)
	{
	}
}