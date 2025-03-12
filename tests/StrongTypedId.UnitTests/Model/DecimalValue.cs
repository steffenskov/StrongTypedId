using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<DecimalValue, decimal>))]
[StrongTypedValueJsonConverter<DecimalValue, decimal>]
public class DecimalValue : StrongTypedValue<DecimalValue, decimal>
{
	public DecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedDecimalValue, decimal>))]
[StrongTypedValueJsonConverter<AttributedDecimalValue, decimal>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDecimalValue, decimal>))]
public class AttributedDecimalValue : StrongTypedValue<AttributedDecimalValue, decimal>
{
	public AttributedDecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}