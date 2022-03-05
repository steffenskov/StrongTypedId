namespace StrongTypedId.UnitTests.Model;

[JsonConverter(typeof(IntId.StrongTypedIdJsonConverter))]
public class IntId : StrongTypedId<IntId, int>
{
	public IntId(int value) : base(value)
	{
	}
}
