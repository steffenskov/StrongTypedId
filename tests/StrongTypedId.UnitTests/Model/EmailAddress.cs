namespace StrongTypedId.UnitTests.Model;

public partial class AttributedEmailAddress : StrongTypedValue<AttributedEmailAddress, string>
{
	public AttributedEmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}

public partial class EmailAddress : StrongTypedValue<EmailAddress, string>
{
	public EmailAddress(string primitiveValue) : base(primitiveValue)
	{
	}
}