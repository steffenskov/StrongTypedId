using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedGuidId, Guid>))]
public partial class AttributedGuidId : StrongTypedGuid<AttributedGuidId>
{
	public AttributedGuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class GuidId : StrongTypedGuid<GuidId>
{
	public GuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}