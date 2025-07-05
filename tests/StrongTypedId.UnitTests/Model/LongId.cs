namespace StrongTypedId.UnitTests.Model;

public partial class LongId : StrongTypedId<LongId, long>
{
	public LongId(long primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedLongId : StrongTypedId<AttributedLongId, long>
{
	public AttributedLongId(long primitiveValue) : base(primitiveValue)
	{
	}
}