using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverter<AttributedEmailAddress, string>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedEmailAddress, string>))]
public class AttributedEmailAddress : StrongTypedValue<AttributedEmailAddress, string>
{
	public AttributedEmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverter<EmailAddress, string>]
public class EmailAddress : StrongTypedValue<EmailAddress, string>
{
	public EmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}