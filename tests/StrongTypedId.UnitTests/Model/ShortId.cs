namespace StrongTypedId.UnitTests.Model;

public partial class ShortId : StrongTypedId<ShortId, short>
{
	public ShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedShortId : StrongTypedId<AttributedShortId, short>
{
	public AttributedShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}