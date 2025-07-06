using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class FloatValue : StrongTypedValue<FloatValue, float>
{
	public FloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedFloatValue, float>))]
public class AttributedFloatValue : StrongTypedValue<AttributedFloatValue, float>
{
	public AttributedFloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}