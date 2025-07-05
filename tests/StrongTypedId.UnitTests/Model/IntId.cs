using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedIdJsonConverter<AttributedIntId, int>]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedIntId, int>))]
public class AttributedIntId : StrongTypedId<AttributedIntId, int>
{
	public AttributedIntId(int primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverter<IntId, int>]
public class IntId : StrongTypedId<IntId, int>
{
	public IntId(int primitiveValue) : base(primitiveValue)
	{
	}
}