namespace StrongTypedId.UnitTests.Model;

[JsonConverter(typeof(SystemTextJsonConverter<GuidId, Guid>))]
[Newtonsoft.Json.JsonConverter(typeof(NewtonSoftJsonConverter<GuidId, Guid>))]
public class GuidId : StrongTypedId<GuidId, Guid>
{
	public GuidId(Guid primitiveId) : base(primitiveId)
	{
	}
}
