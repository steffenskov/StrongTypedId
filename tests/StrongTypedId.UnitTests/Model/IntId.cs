namespace StrongTypedId.UnitTests.Model;

[StrongTypedIdJsonConverter<IntId, int>]
[Newtonsoft.Json.JsonConverter(typeof(NewtonSoftJsonConverter<IntId, int>))]
public class IntId : StrongTypedId<IntId, int>
{
	public IntId(int primitiveValue) : base(primitiveValue)
	{
	}
}
