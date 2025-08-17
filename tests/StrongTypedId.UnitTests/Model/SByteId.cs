using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedSByteId, sbyte>))]
public class AttributedSByteId : StrongTypedId<AttributedSByteId, sbyte>
{
	public AttributedSByteId(sbyte primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
public class SByteId : StrongTypedId<SByteId, sbyte>
{
	public SByteId(sbyte primitiveValue) : base(primitiveValue)
	{
	}
}