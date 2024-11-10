namespace StrongTypedId.IntegrationTests.Models;

public class StrongDoubleId : StrongTypedId<StrongDoubleId, double>
{
	public StrongDoubleId(double primitiveId) : base(primitiveId)
	{
	}
}