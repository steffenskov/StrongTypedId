namespace StrongTypedId.IntegrationTests.Models;

public class StrongStringValue: StrongTypedValue<StrongStringValue, string>
{
	public StrongStringValue(string primitiveValue) : base(primitiveValue)
	{
	}
}