using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverter<FloatValue, float>]
public class FloatValue : StrongTypedValue<FloatValue, float>
{
	public FloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverter<AttributedFloatValue, float>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedFloatValue, float>))]
public class AttributedFloatValue : StrongTypedValue<AttributedFloatValue, float>
{
	public AttributedFloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}