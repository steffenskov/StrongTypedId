namespace StrongTypedId.UnitTests.Model;

public class EmailAddress : StrongTypedValue<EmailAddress, string>
{
    public EmailAddress(string primitiveId) : base(primitiveId)
    {
    }
}