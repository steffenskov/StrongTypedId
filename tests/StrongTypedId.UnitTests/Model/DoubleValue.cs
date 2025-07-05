namespace StrongTypedId.UnitTests.Model;

public partial class DoubleValue : StrongTypedValue<DoubleValue, double>
{
	public DoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedDoubleValue : StrongTypedValue<AttributedDoubleValue, double>
{
	public AttributedDoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}