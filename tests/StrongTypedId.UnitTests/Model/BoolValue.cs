using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverter<BoolValue, bool>]
public class BoolValue : StrongTypedValue<BoolValue, bool>
{
	public BoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverter<AttributedBoolValue, bool>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedBoolValue, bool>))]
public class AttributedBoolValue : StrongTypedValue<AttributedBoolValue, bool>
{
	public AttributedBoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}