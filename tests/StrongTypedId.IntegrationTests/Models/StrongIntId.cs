namespace StrongTypedId.IntegrationTests.Models;

public class StrongIntId : StrongTypedId<StrongIntId, int>
{
	public StrongIntId(int primitiveId) : base(primitiveId)
	{
	}
}