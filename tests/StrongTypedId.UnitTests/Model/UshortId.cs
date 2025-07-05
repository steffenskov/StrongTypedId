namespace StrongTypedId.UnitTests.Model;

public partial class UshortId : StrongTypedId<UshortId, ushort>
{
	public UshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedUshortId : StrongTypedId<AttributedUshortId, ushort>
{
	public AttributedUshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}