namespace StrongTypedId.UnitTests.Model;

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