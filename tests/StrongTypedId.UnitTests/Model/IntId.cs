namespace StrongTypedId.UnitTests.Model;

public partial class AttributedIntId : StrongTypedId<AttributedIntId, int>
{
	public AttributedIntId(int primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class IntId : StrongTypedId<IntId, int>
{
	public IntId(int primitiveValue) : base(primitiveValue)
	{
	}
}