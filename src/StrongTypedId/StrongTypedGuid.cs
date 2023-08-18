using System;

namespace StrongTypedId
{
    /// <Summary>
    /// Abstract baseclass to represent a strong typed guid. Use it like this:
    /// public class UserId: StrongTypedId&lt;UserId&gt;
    /// </Summary>
    public abstract class StrongTypedGuid<TSelf> : StrongTypedId<TSelf, Guid>
        where TSelf : StrongTypedGuid<TSelf>
    {
        /// <Summary>
        /// Creates a new instance of your strong typed id with Guid.NewGuid() as its primitive id.
        /// </Summary>
        public static TSelf New()
        {
            return Create(Guid.NewGuid());
        }

        public static TSelf Empty { get; } = Create(Guid.Empty);

        protected StrongTypedGuid(Guid primitiveId) : base(primitiveId)
        {
        }
    }
}