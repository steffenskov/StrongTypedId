using System;
using StrongTypedId;

namespace Dapper.DDD.Repository
{
	public static class Extensions
	{
		public static (Func<TStrongTypedValue, TPrimitiveValue> convertToPrimitive, Func<TPrimitiveValue, TStrongTypedValue> convertToStrongType) GetTypeConverter<TStrongTypedValue, TPrimitiveValue>(this StrongTypedValue<TStrongTypedValue, TPrimitiveValue> strongTypedValue)
			where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, TPrimitiveValue>
			where TPrimitiveValue :  IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
		{
			return ((id) => id.PrimitiveValue, StrongTypedValue<TStrongTypedValue, TPrimitiveValue>.Create);
		}
	}
}