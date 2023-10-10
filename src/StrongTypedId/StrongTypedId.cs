using System;

namespace StrongTypedId
{
    /// <Summary>
    /// Abstract baseclass to represent a strong typed id. Use it like this:
    /// public class UserId: StrongTypedId&lt;UserId, int&gt;
    /// </Summary>
    public abstract class StrongTypedId<TSelf, TPrimitiveId> : StrongTypedValue<TSelf, TPrimitiveId>,
        IParsable<TSelf?>
        where TSelf : StrongTypedId<TSelf, TPrimitiveId>
        where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
        IParsable<TPrimitiveId>
    {
        protected StrongTypedId(TPrimitiveId value) : base(value)
        {
        }

        public TPrimitiveId PrimitiveId => Value;
        
        public static TSelf Parse(string s, IFormatProvider? provider = null)
        {
            return Create(TPrimitiveId.Parse(s, provider));
        }

        public static bool TryParse(string? s, out TSelf? result)
        {
            return TryParse(s, null, out result);
        }

        public static bool TryParse(string? s, IFormatProvider? provider, out TSelf? result)
        {
            if (TPrimitiveId.TryParse(s, provider, out var primitiveId))
            {
                result = Create(primitiveId);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }
}