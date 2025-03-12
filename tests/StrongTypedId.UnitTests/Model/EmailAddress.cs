using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedEmailAddress, string>))]
[StrongTypedValueJsonConverter<AttributedEmailAddress, string>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedEmailAddress, string>))]
public class AttributedEmailAddress : StrongTypedValue<AttributedEmailAddress, string>
{
	public AttributedEmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<EmailAddress, string>))]
[StrongTypedValueJsonConverter<EmailAddress, string>]
public class EmailAddress : StrongTypedValue<EmailAddress, string>
{
	public EmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}