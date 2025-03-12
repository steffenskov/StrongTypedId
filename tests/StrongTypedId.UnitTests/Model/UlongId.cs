using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<UlongId, ulong>))]
[StrongTypedIdJsonConverter<UlongId, ulong>]
public class UlongId : StrongTypedId<UlongId, ulong>
{
	public UlongId(ulong primitiveValue) : base(primitiveValue)
	{
	}
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<AttributedUlongId, ulong>))]
[StrongTypedIdJsonConverter<AttributedUlongId, ulong>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUlongId, ulong>))]
public class AttributedUlongId : StrongTypedId<AttributedUlongId, ulong>
{
	public AttributedUlongId(ulong primitiveValue) : base(primitiveValue)
	{
	}
}