using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class DateValue : StrongTypedValue<DateValue, DateTime>
{
	public DateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDateValue, DateTime>))]
public class AttributedDateValue : StrongTypedValue<AttributedDateValue, DateTime>
{
	public AttributedDateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}