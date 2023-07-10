using System;
using StrongTypedId;

namespace Dapper.DDD.Repository
{
	public static class Extensions
	{
		public static (Func<TStrongTypedValue, TPrimitiveId> convertToPrimitive, Func<TPrimitiveId, TStrongTypedValue> convertToStrongType) GetTypeConverter<TStrongTypedValue, TPrimitiveId>(this StrongTypedValue<TStrongTypedValue, TPrimitiveId> strongTypedValue)
			where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, TPrimitiveId>
			where TPrimitiveId :  IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>, IParsable<TPrimitiveId>
		{
			return ((id) => id.PrimitiveId, StrongTypedValue<TStrongTypedValue, TPrimitiveId>.Create);
		}
	}
}