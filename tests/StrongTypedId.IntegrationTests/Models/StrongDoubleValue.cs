namespace StrongTypedId.IntegrationTests.Models;

public class StrongDoubleValue : StrongTypedValue<StrongDoubleValue, double>
{
	public StrongDoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}