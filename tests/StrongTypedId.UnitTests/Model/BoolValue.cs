namespace StrongTypedId.UnitTests.Model;

public partial class BoolValue : StrongTypedValue<BoolValue, bool>
{
	public BoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedBoolValue : StrongTypedValue<AttributedBoolValue, bool>
{
	public AttributedBoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}