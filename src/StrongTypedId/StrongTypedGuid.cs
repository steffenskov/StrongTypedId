using System;

namespace StrongTypedId
{
    /// <Summary>
    /// Abstract baseclass to represent a strong typed guid. Use it like this:
    /// public class UserId: StrongTypedId&lt;UserId&gt;
    /// </Summary>
    public abstract class StrongTypedGuid<TStrongTypedId> : StrongTypedId<TStrongTypedId, Guid>
        where TStrongTypedId : StrongTypedGuid<TStrongTypedId>
    {
        /// <Summary>
        /// Creates a new instance of your strong typed id with Guid.NewGuid() as its primitive id.
        /// </Summary>
        public static TStrongTypedId New()
        {
            return Create(Guid.NewGuid());
        }

        public static TStrongTypedId Empty { get; } = Create(Guid.Empty);

        protected StrongTypedGuid(Guid primitiveId) : base(primitiveId)
        {
        }
    }
}