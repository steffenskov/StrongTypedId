namespace StrongTypedId.UnitTests.Model;

[JsonConverter(typeof(SystemTextJsonConverter<GuidId, Guid>))]
[Newtonsoft.Json.JsonConverter(typeof(NewtonSoftJsonConverter<GuidId, Guid>))]
public class GuidId : StrongTypedGuid<GuidId>
{
	public GuidId(Guid primitiveId) : base(primitiveId)
	{
	}
}
