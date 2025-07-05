using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedIdJsonConverter<UshortId, ushort>]
public class UshortId : StrongTypedId<UshortId, ushort>
{
	public UshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedIdJsonConverter<AttributedUshortId, ushort>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUshortId, ushort>))]
public class AttributedUshortId : StrongTypedId<AttributedUshortId, ushort>
{
	public AttributedUshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}