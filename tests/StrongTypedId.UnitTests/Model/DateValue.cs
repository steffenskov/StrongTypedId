using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<DateValue, DateTime>))]
[StrongTypedValueJsonConverter<DateValue, DateTime>]
public class DateValue : StrongTypedValue<DateValue, DateTime>
{
	public DateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedDateValue, DateTime>))]
[StrongTypedValueJsonConverter<AttributedDateValue, DateTime>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDateValue, DateTime>))]
public class AttributedDateValue : StrongTypedValue<AttributedDateValue, DateTime>
{
	public AttributedDateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}