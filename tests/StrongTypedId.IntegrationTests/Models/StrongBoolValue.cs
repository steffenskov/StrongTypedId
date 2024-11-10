namespace StrongTypedId.IntegrationTests.Models;

public class StrongBoolValue: StrongTypedValue<StrongBoolValue, bool>
{
	public StrongBoolValue(bool primitiveValue) : base(primitiveValue)
	{
	}
}