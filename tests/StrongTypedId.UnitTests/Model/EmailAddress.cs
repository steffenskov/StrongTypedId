using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<EmailAddress, string>))]
[StrongTypedValueJsonConverter<EmailAddress, string>]
[JsonConverter(typeof(NewtonSoftJsonConverter<EmailAddress, string>))]
public class EmailAddress : StrongTypedValue<EmailAddress, string>
{
	public EmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}

	public static StrongTypedValueJsonConverterAttribute<EmailAddress, string> JsonConverter { get; } = new();
}