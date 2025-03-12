namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<DateValue, DateTime>))]
[StrongTypedValueJsonConverter<DateValue, DateTime>]
public class DateValue : StrongTypedValue<DateValue, DateTime>
{
	public DateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}