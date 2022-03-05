namespace StrongTypedId.UnitTests.Model;

[JsonConverter(typeof(IntId.StrongTypedIdJsonConverter))]
public class GuidId : StrongTypedId<GuidId, Guid>
{
	public GuidId(Guid value) : base(value)
	{
	}
}
