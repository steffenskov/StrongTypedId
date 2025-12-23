namespace StrongTypedId;

/// <Summary>
///     Abstract baseclass to represent a strong typed guid. Use it like this:
///     public class UserId: StrongTypedId&lt;UserId&gt;
/// </Summary>
public abstract class StrongTypedGuid<TSelf> : StrongTypedId<TSelf, Guid>, IStrongTypedGuid<TSelf>
	where TSelf : StrongTypedGuid<TSelf>
{
	protected StrongTypedGuid(Guid primitiveValue) : base(primitiveValue)
	{
	}
}