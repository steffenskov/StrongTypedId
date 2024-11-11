namespace StrongTypedId.IntegrationTests.Models;

public class StrongGuidId: StrongTypedId<StrongGuidId, Guid>
{
	public StrongGuidId(Guid primitiveId) : base(primitiveId)
	{
	}
}