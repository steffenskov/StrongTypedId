using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class UshortId : StrongTypedId<UshortId, ushort>
{
	public UshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUshortId, ushort>))]
public class AttributedUshortId : StrongTypedId<AttributedUshortId, ushort>
{
	public AttributedUshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}