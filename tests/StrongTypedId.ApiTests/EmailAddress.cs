using Newtonsoft.Json;
using StrongTypedId.Converters;

namespace StrongTypedId.ApiTests;

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<EmailAddress, string>))]
public class EmailAddress : StrongTypedValue<EmailAddress, string>
{
	public EmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}