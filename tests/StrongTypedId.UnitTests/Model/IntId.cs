using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<IntId, int>))]
[StrongTypedIdJsonConverter<IntId, int>]
[JsonConverter(typeof(NewtonSoftJsonConverter<IntId, int>))]
public class IntId : StrongTypedId<IntId, int>
{
	public IntId(int primitiveValue) : base(primitiveValue)
	{
	}
}