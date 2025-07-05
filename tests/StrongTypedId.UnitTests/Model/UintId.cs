using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

public partial class UintId : StrongTypedId<UintId, uint>
{
	public UintId(uint primitiveValue) : base(primitiveValue)
	{
	}
}

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUintId, uint>))]
public partial class AttributedUintId : StrongTypedId<AttributedUintId, uint>
{
	public AttributedUintId(uint primitiveValue) : base(primitiveValue)
	{
	}
}