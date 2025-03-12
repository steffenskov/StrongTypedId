using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<UshortId, ushort>))]
[StrongTypedIdJsonConverter<UshortId, ushort>]
public class UshortId : StrongTypedId<UshortId, ushort>
{
	public UshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedUshortId, ushort>))]
[StrongTypedIdJsonConverter<AttributedUshortId, ushort>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUshortId, ushort>))]
public class AttributedUshortId : StrongTypedId<AttributedUshortId, ushort>
{
	public AttributedUshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}