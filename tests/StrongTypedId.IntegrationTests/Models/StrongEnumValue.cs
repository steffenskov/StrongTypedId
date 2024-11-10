namespace StrongTypedId.IntegrationTests.Models;

public class StrongEnumValue : StrongTypedEnum<StrongEnumValue, FakeEnum>
{
	public StrongEnumValue(FakeEnum value) : base(value)
	{
	}

	public StrongEnumValue(string value) : base(value)
	{
	}
}

public enum FakeEnum
{
	Value1 = 1,
	Value2 = 2
}