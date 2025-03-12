using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<FloatValue, float>))]
[StrongTypedValueJsonConverter<FloatValue, float>]
public class FloatValue : StrongTypedValue<FloatValue, float>
{
	public FloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedFloatValue, float>))]
[StrongTypedValueJsonConverter<AttributedFloatValue, float>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedFloatValue, float>))]
public class AttributedFloatValue : StrongTypedValue<AttributedFloatValue, float>
{
	public AttributedFloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}