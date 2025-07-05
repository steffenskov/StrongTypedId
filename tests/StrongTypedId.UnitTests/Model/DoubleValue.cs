using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

public partial class DoubleValue : StrongTypedValue<DoubleValue, double>
{
	public DoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDoubleValue, double>))]
public partial class AttributedDoubleValue : StrongTypedValue<AttributedDoubleValue, double>
{
	public AttributedDoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}