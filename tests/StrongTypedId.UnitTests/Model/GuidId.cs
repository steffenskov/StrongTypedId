using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedGuidId, Guid>))]
[StrongTypedIdJsonConverter<AttributedGuidId, Guid>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedGuidId, Guid>))]
public class AttributedGuidId : StrongTypedGuid<AttributedGuidId>
{
	public AttributedGuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<GuidId, Guid>))]
[StrongTypedIdJsonConverter<GuidId, Guid>]
public class GuidId : StrongTypedGuid<GuidId>
{
	public GuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}