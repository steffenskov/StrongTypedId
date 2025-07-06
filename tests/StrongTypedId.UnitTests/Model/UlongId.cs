using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class UlongId : StrongTypedId<UlongId, ulong>
{
	public UlongId(ulong primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUlongId, ulong>))]
public class AttributedUlongId : StrongTypedId<AttributedUlongId, ulong>
{
	public AttributedUlongId(ulong primitiveValue) : base(primitiveValue)
	{
	}
}