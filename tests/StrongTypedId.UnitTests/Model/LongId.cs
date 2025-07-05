using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

public partial class LongId : StrongTypedId<LongId, long>
{
	public LongId(long primitiveValue) : base(primitiveValue)
	{
	}
}

[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedLongId, long>))]
public partial class AttributedLongId : StrongTypedId<AttributedLongId, long>
{
	public AttributedLongId(long primitiveValue) : base(primitiveValue)
	{
	}
}