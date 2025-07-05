namespace StrongTypedId.UnitTests.Model;

public partial class DecimalValue : StrongTypedValue<DecimalValue, decimal>
{
	public DecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedDecimalValue : StrongTypedValue<AttributedDecimalValue, decimal>
{
	public AttributedDecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}