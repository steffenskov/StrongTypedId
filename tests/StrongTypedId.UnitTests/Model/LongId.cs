using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
public class LongId : StrongTypedId<LongId, long>
{
	public LongId(long primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedLongId, long>))]
public class AttributedLongId : StrongTypedId<AttributedLongId, long>
{
	public AttributedLongId(long primitiveValue) : base(primitiveValue)
	{
	}
}