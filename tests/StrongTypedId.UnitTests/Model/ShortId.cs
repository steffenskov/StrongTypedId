using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedIdJsonConverter<ShortId, short>]
public class ShortId : StrongTypedId<ShortId, short>
{
	public ShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedIdJsonConverter<AttributedShortId, short>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedShortId, short>))]
public class AttributedShortId : StrongTypedId<AttributedShortId, short>
{
	public AttributedShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}