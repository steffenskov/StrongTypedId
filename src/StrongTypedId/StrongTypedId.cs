using System;

namespace StrongTypedId
{
    /// <Summary>
    /// Abstract baseclass to represent a strong typed id. Use it like this:
    /// public class UserId: StrongTypedId&lt;UserId, int&gt;
    /// </Summary>
    public abstract class StrongTypedId<TStrongTypedId, TPrimitiveId> : StrongTypedValue<TStrongTypedId, TPrimitiveId>
        where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
        where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
        IParsable<TPrimitiveId>
    {
        protected StrongTypedId(TPrimitiveId primitiveId) : base(primitiveId)
        {
        }
    }
}