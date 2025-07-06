using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class BoolValue : StrongTypedValue<BoolValue, bool>
{
	public BoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedBoolValue, bool>))]
public class AttributedBoolValue : StrongTypedValue<AttributedBoolValue, bool>
{
	public AttributedBoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}