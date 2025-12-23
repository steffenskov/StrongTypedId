namespace StrongTypedId;

/// <Summary>
///     Abstract baseclass to represent a strong typed id. Use it like this:
///     public class UserId: StrongTypedId&lt;UserId, int&gt;
/// </Summary>
public abstract class StrongTypedId<TSelf, TPrimitiveId> : StrongTypedValue<TSelf, TPrimitiveId>,
	IStrongTypedId<TSelf, TPrimitiveId>
	where TSelf : StrongTypedId<TSelf, TPrimitiveId>
	where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
	IParsable<TPrimitiveId>
{
	protected StrongTypedId(TPrimitiveId value) : base(value)
	{
	}
}