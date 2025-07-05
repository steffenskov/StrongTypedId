using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

public partial class UlongId : StrongTypedId<UlongId, ulong>
{
	public UlongId(ulong primitiveValue) : base(primitiveValue)
	{
	}
}

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedUlongId, ulong>))]
public partial class AttributedUlongId : StrongTypedId<AttributedUlongId, ulong>
{
	public AttributedUlongId(ulong primitiveValue) : base(primitiveValue)
	{
	}
}