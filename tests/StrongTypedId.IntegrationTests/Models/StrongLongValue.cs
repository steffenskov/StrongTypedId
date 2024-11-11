namespace StrongTypedId.IntegrationTests.Models;

public class StrongLongValue : StrongTypedValue<StrongLongValue, long>
{
	public StrongLongValue(long primitiveValue) : base(primitiveValue)
	{
	}
}