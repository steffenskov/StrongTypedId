using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedByteId, byte>))]
public class AttributedByteId : StrongTypedId<AttributedByteId, byte>
{
	public AttributedByteId(byte primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
public class ByteId : StrongTypedId<ByteId, byte>
{
	public ByteId(byte primitiveValue) : base(primitiveValue)
	{
	}
}