namespace StrongTypedId.UnitTests.Model;

public class EmailAddress : StrongTypedValue<EmailAddress, string>
{
    public EmailAddress(string primitiveValue) : base(primitiveValue)
    {
    }
}