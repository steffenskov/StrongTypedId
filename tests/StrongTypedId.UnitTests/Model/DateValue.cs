namespace StrongTypedId.UnitTests.Model;

public partial class DateValue : StrongTypedValue<DateValue, DateTime>
{
	public DateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedDateValue : StrongTypedValue<AttributedDateValue, DateTime>
{
	public AttributedDateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}