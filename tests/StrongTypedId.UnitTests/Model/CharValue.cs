using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverter<CharValue, char>]
public class CharValue : StrongTypedValue<CharValue, char>
{
	public CharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverter<AttributedCharValue, char>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedCharValue, char>))]
public class AttributedCharValue : StrongTypedValue<AttributedCharValue, char>
{
	public AttributedCharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}