using System;
using System.ComponentModel;
using System.Globalization;

namespace StrongTypedId.Converters
{
	/// <summary>
	/// TypeConverter for WebAPI and MVC. Its purpose is to allow StrongTypedIds as arguments to controller actions.
	/// Use it like this:
	/// [TypeConverter(typeof(StrongTypedIdTypeConverter<UserId, Guid>))]
	/// public class UserId: StrongTypedId<UserId, Guid>
	/// </summary>
	public class StrongTypedIdTypeConverter<TStrongTypedId, TPrimitiveId> : TypeConverter
		where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>
	{
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			var stringValue = value as string;
			if (!string.IsNullOrEmpty(stringValue) && TryParse(stringValue, out var primitiveId))
			{
				return StrongTypedId<TStrongTypedId, TPrimitiveId>.Create((TPrimitiveId)primitiveId);
			}

			return base.ConvertFrom(context, culture, value);
		}

		private static bool TryParse(string stringValue, out object primitiveId)
		{
			primitiveId = typeof(TPrimitiveId) switch
			{
				Type t when t == typeof(bool) => bool.Parse(stringValue),
				Type t when t == typeof(Guid) => Guid.Parse(stringValue),
				Type t when t == typeof(short) => short.Parse(stringValue),
				Type t when t == typeof(int) => int.Parse(stringValue),
				Type t when t == typeof(long) => long.Parse(stringValue),
				Type t when t == typeof(ushort) => ushort.Parse(stringValue),
				Type t when t == typeof(uint) => uint.Parse(stringValue),
				Type t when t == typeof(ulong) => ulong.Parse(stringValue),
				Type t when t == typeof(float) => float.Parse(stringValue),
				Type t when t == typeof(double) => double.Parse(stringValue),
				Type t when t == typeof(decimal) => decimal.Parse(stringValue),
				Type t when t == typeof(byte) => byte.Parse(stringValue),
				Type t when t == typeof(sbyte) => sbyte.Parse(stringValue),
				_ => throw new NotSupportedException()
			};
			return true;
		}
	}
}