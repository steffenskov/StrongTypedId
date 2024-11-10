namespace StrongTypedId.IntegrationTests.Models;

public class StrongLongId : StrongTypedId<StrongLongId, long>
{
	public StrongLongId(long primitiveId) : base(primitiveId)
	{
	}
}