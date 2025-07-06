using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class CharValue : StrongTypedValue<CharValue, char>
{
	public CharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedCharValue, char>))]
public class AttributedCharValue : StrongTypedValue<AttributedCharValue, char>
{
	public AttributedCharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}