using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class ShortId : StrongTypedId<ShortId, short>
{
	public ShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedShortId, short>))]
public class AttributedShortId : StrongTypedId<AttributedShortId, short>
{
	public AttributedShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}