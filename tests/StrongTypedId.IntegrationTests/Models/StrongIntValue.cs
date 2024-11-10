namespace StrongTypedId.IntegrationTests.Models;

public class StrongIntValue : StrongTypedValue<StrongIntValue, int>
{
	public StrongIntValue(int primitiveValue) : base(primitiveValue)
	{
	}
}