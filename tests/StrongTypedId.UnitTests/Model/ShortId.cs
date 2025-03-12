using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<ShortId, short>))]
[StrongTypedIdJsonConverter<ShortId, short>]
public class ShortId : StrongTypedId<ShortId, short>
{
	public ShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedShortId, short>))]
[StrongTypedIdJsonConverter<AttributedShortId, short>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedShortId, short>))]
public class AttributedShortId : StrongTypedId<AttributedShortId, short>
{
	public AttributedShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}