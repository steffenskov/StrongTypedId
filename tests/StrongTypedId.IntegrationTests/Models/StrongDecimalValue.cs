namespace StrongTypedId.IntegrationTests.Models;

public class StrongDecimalValue : StrongTypedValue<StrongDecimalValue, decimal>
{
	public StrongDecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}