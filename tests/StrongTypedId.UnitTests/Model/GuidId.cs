namespace StrongTypedId.UnitTests.Model;

[StrongTypedIdJsonConverter<GuidId, Guid>]
[Newtonsoft.Json.JsonConverter(typeof(NewtonSoftJsonConverter<GuidId, Guid>))]
public class GuidId : StrongTypedGuid<GuidId>
{
	public GuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}
