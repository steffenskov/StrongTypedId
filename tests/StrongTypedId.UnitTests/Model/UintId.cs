using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedIdJsonConverter<UintId, uint>]
public class UintId : StrongTypedId<UintId, uint>
{
	public UintId(uint primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedIdJsonConverter<AttributedUintId, uint>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUintId, uint>))]
public class AttributedUintId : StrongTypedId<AttributedUintId, uint>
{
	public AttributedUintId(uint primitiveValue) : base(primitiveValue)
	{
	}
}