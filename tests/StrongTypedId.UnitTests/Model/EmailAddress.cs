using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedEmailAddress, string>))]
public class AttributedEmailAddress : StrongTypedValue<AttributedEmailAddress, string>
{
	public AttributedEmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
public class EmailAddress : StrongTypedValue<EmailAddress, string>
{
	public EmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}