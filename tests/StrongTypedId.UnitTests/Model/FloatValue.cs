namespace StrongTypedId.UnitTests.Model;

public partial class FloatValue : StrongTypedValue<FloatValue, float>
{
	public FloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedFloatValue : StrongTypedValue<AttributedFloatValue, float>
{
	public AttributedFloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}