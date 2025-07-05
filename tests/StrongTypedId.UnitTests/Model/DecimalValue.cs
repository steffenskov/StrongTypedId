using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverter<DecimalValue, decimal>]
public class DecimalValue : StrongTypedValue<DecimalValue, decimal>
{
	public DecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverter<AttributedDecimalValue, decimal>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDecimalValue, decimal>))]
public class AttributedDecimalValue : StrongTypedValue<AttributedDecimalValue, decimal>
{
	public AttributedDecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}