using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<GuidId, Guid>))]
[StrongTypedIdJsonConverter<GuidId, Guid>]
[JsonConverter(typeof(NewtonSoftJsonConverter<GuidId, Guid>))]
public class GuidId : StrongTypedGuid<GuidId>
{
	public GuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}