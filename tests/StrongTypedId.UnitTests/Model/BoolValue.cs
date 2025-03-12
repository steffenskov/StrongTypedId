using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<BoolValue, bool>))]
[StrongTypedValueJsonConverter<BoolValue, bool>]
public class BoolValue : StrongTypedValue<BoolValue, bool>
{
	public BoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedBoolValue, bool>))]
[StrongTypedValueJsonConverter<AttributedBoolValue, bool>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedBoolValue, bool>))]
public class AttributedBoolValue : StrongTypedValue<AttributedBoolValue, bool>
{
	public AttributedBoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}