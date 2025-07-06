using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class DecimalValue : StrongTypedValue<DecimalValue, decimal>
{
	public DecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDecimalValue, decimal>))]
public class AttributedDecimalValue : StrongTypedValue<AttributedDecimalValue, decimal>
{
	public AttributedDecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}