using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<DoubleValue, double>))]
[StrongTypedValueJsonConverter<DoubleValue, double>]
public class DoubleValue : StrongTypedValue<DoubleValue, double>
{
	public DoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedDoubleValue, double>))]
[StrongTypedValueJsonConverter<AttributedDoubleValue, double>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDoubleValue, double>))]
public class AttributedDoubleValue : StrongTypedValue<AttributedDoubleValue, double>
{
	public AttributedDoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}