namespace StrongTypedId.IntegrationTests.Models;

public class StrongGuidValue: StrongTypedValue<StrongGuidValue, Guid>
{
	public StrongGuidValue(Guid primitiveValue) : base(primitiveValue)
	{
	}
}