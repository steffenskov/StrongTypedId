namespace StrongTypedId.ApiTests;

public class WeatherId : StrongTypedGuid<WeatherId>
{
	public WeatherId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}