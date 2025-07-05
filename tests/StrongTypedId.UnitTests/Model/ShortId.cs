using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

public partial class ShortId : StrongTypedId<ShortId, short>
{
	public ShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedShortId, short>))]
public partial class AttributedShortId : StrongTypedId<AttributedShortId, short>
{
	public AttributedShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}