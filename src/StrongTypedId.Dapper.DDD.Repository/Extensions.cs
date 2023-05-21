using System;
using StrongTypedId;

namespace Dapper.DDD.Repository
{
	public static class Extensions
	{
		public static (Func<TStrongTypedId, TPrimitiveId> convertToPrimitive, Func<TPrimitiveId, TStrongTypedId> convertToStrongType) GetTypeConverter<TStrongTypedId, TPrimitiveId>(this StrongTypedId<TStrongTypedId, TPrimitiveId> strongTypedId)
			where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
			where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>, IParsable<TPrimitiveId>
		{
			return ((id) => id.PrimitiveId, StrongTypedId<TStrongTypedId, TPrimitiveId>.Create);
		}
	}
}