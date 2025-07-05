namespace StrongTypedId.UnitTests.Model;

public partial class CharValue : StrongTypedValue<CharValue, char>
{
	public CharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class AttributedCharValue : StrongTypedValue<AttributedCharValue, char>
{
	public AttributedCharValue(char primitiveValue) : base(primitiveValue)
	{
	}
}