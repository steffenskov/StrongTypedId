namespace StrongTypedId.UnitTests.Model;

[JsonConverter(typeof(SystemTextJsonConverter<IntId, int>))]
public class IntId : StrongTypedId<IntId, int>
{
	public IntId(int value) : base(value)
	{
	}
}
