using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

public partial class DateValue : StrongTypedValue<DateValue, DateTime>
{
	public DateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedDateValue, DateTime>))]
public partial class AttributedDateValue : StrongTypedValue<AttributedDateValue, DateTime>
{
	public AttributedDateValue(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}