using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<LongId, long>))]
[StrongTypedIdJsonConverter<LongId, long>]
public class LongId : StrongTypedId<LongId, long>
{
	public LongId(long primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedLongId, long>))]
[StrongTypedIdJsonConverter<AttributedLongId, long>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedLongId, long>))]
public class AttributedLongId : StrongTypedId<AttributedLongId, long>
{
	public AttributedLongId(long primitiveValue) : base(primitiveValue)
	{
	}
}