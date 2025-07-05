using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

public partial class BoolValue : StrongTypedValue<BoolValue, bool>
{
	public BoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedBoolValue, bool>))]
public partial class AttributedBoolValue : StrongTypedValue<AttributedBoolValue, bool>
{
	public AttributedBoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}