namespace StrongTypedId.IntegrationTests.Models;

public class StrongBoolId: StrongTypedId<StrongBoolId, bool>
{
	public StrongBoolId(bool primitiveId) : base(primitiveId)
	{
	}
}