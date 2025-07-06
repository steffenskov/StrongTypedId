using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class UintId : StrongTypedId<UintId, uint>
{
	public UintId(uint primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUintId, uint>))]
public class AttributedUintId : StrongTypedId<AttributedUintId, uint>
{
	public AttributedUintId(uint primitiveValue) : base(primitiveValue)
	{
	}
}