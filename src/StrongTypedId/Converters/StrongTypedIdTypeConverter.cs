using System;
using System.ComponentModel;
using System.Globalization;

namespace StrongTypedId.Converters
{
	/// <summary>
	/// TypeConverter for WebAPI and MVC. Its purpose is to allow StrongTypedIds as arguments to controller actions.
	/// Use it like this:
	/// [TypeConverter(typeof(StrongTypedIdTypeConverter&lt;UserId, Guid&gt;))]
	/// public class UserId: StrongTypedId&lt;UserId, Guid&gt;
	/// </summary>
	public class StrongTypedValueTypeConverter<TStrongTypedValue, TPrimitiveValue> : TypeConverter
		where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, TPrimitiveValue>
		where TPrimitiveValue :  IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>, IParsable<TPrimitiveValue>
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
				return StrongTypedValue<TStrongTypedValue, TPrimitiveValue>.Create((TPrimitiveValue)primitiveId);
			}

			return base.ConvertFrom(context, culture, value);
		}

		private static bool TryParse(string stringValue, out object primitiveId)
		{
			primitiveId = typeof(TPrimitiveValue) switch
			{
				{ } t when t == typeof(bool) => bool.Parse(stringValue),
				{ } t when t == typeof(char) => stringValue[0],
				{ } t when t == typeof(Guid) => Guid.Parse(stringValue),
				{ } t when t == typeof(short) => short.Parse(stringValue),
				{ } t when t == typeof(int) => int.Parse(stringValue),
				{ } t when t == typeof(long) => long.Parse(stringValue),
				{ } t when t == typeof(ushort) => ushort.Parse(stringValue),
				{ } t when t == typeof(uint) => uint.Parse(stringValue),
				{ } t when t == typeof(ulong) => ulong.Parse(stringValue),
				{ } t when t == typeof(float) => float.Parse(stringValue),
				{ } t when t == typeof(double) => double.Parse(stringValue),
				{ } t when t == typeof(decimal) => decimal.Parse(stringValue),
				{ } t when t == typeof(byte) => byte.Parse(stringValue),
				{ } t when t == typeof(sbyte) => sbyte.Parse(stringValue),
				_ => throw new NotSupportedException()
			};
			return true;
		}
	}
}