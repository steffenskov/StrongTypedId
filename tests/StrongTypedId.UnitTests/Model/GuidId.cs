namespace StrongTypedId.UnitTests.Model;

public partial class AttributedGuidId : StrongTypedGuid<AttributedGuidId>
{
	public AttributedGuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class GuidId : StrongTypedGuid<GuidId>
{
	public GuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}