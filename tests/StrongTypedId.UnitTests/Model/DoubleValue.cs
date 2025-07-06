using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class DoubleValue : StrongTypedValue<DoubleValue, double>
{
	public DoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDoubleValue, double>))]
public class AttributedDoubleValue : StrongTypedValue<AttributedDoubleValue, double>
{
	public AttributedDoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}