using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverter<DoubleValue, double>]
public class DoubleValue : StrongTypedValue<DoubleValue, double>
{
	public DoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverter<AttributedDoubleValue, double>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDoubleValue, double>))]
public class AttributedDoubleValue : StrongTypedValue<AttributedDoubleValue, double>
{
	public AttributedDoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}