using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

public partial class CharValue : StrongTypedValue<CharValue, char>
{
	public CharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedCharValue, char>))]
public partial class AttributedCharValue : StrongTypedValue<AttributedCharValue, char>
{
	public AttributedCharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}