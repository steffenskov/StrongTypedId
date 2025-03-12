using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<CharValue, char>))]
[StrongTypedValueJsonConverter<CharValue, char>]
public class CharValue : StrongTypedValue<CharValue, char>
{
	public CharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedCharValue, char>))]
[StrongTypedValueJsonConverter<AttributedCharValue, char>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedCharValue, char>))]
public class AttributedCharValue : StrongTypedValue<AttributedCharValue, char>
{
	public AttributedCharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}