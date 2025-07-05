using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedByteId, byte>))]
public partial class AttributedByteId : StrongTypedId<AttributedByteId, byte>
{
	public AttributedByteId(byte primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class ByteId : StrongTypedId<ByteId, byte>
{
	public ByteId(byte primitiveValue) : base(primitiveValue)
	{
	}
}