using StrongTypedId.Reflection;

namespace StrongTypedId;

public static class StrongTypedExtensions
{
	extension(StrongTypedValue)
	{
		public static object Create(Type type, object primitiveValue)
		{
			return DynamicActivator.Create(type, primitiveValue);
		}
	}

	extension<TSelf, TPrimitiveValue>(IStrongTypedValue<TSelf, TPrimitiveValue>)
		where TSelf : IStrongTypedValue<TSelf, TPrimitiveValue>
		where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
	{
		public static TSelf Create(TPrimitiveValue value)
		{
			return DynamicActivator.GetActivator<TSelf, TPrimitiveValue>().Create(value); // TODO: Suffers performance hit b/c of double dictionary lookup, consider different design
		}

		public static bool operator ==(IStrongTypedValue<TSelf, TPrimitiveValue>? a,
			IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			if (a is null && b is null)
			{
				return true;
			}

			return a?.PrimitiveValue.Equals(b is null ? null : b.PrimitiveValue) == true;
		}

		public static bool operator ==(IStrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
		{
			return a?.PrimitiveValue.Equals(b) == true;
		}

		public static bool operator ==(TPrimitiveValue? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.Equals(b is null ? null : b.PrimitiveValue) == true;
		}

		public static bool operator >(IStrongTypedValue<TSelf, TPrimitiveValue>? a,
			IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) > 0;
		}

		public static bool operator >(IStrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
		{
			return a?.PrimitiveValue.CompareTo(b) > 0;
		}

		public static bool operator >(TPrimitiveValue? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.CompareTo(b is null ? null : b.PrimitiveValue) > 0;
		}

		public static bool operator <(IStrongTypedValue<TSelf, TPrimitiveValue>? a,
			IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) < 0;
		}

		public static bool operator <(IStrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
		{
			return a?.PrimitiveValue.CompareTo(b) < 0;
		}

		public static bool operator <(TPrimitiveValue? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.CompareTo(b is null ? null : b.PrimitiveValue) < 0;
		}

		public static bool operator >=(IStrongTypedValue<TSelf, TPrimitiveValue>? a,
			IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) >= 0;
		}

		public static bool operator >=(IStrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
		{
			return a?.PrimitiveValue.CompareTo(b) >= 0;
		}

		public static bool operator >=(TPrimitiveValue? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.CompareTo(b is null ? null : b.PrimitiveValue) >= 0;
		}

		public static bool operator <=(IStrongTypedValue<TSelf, TPrimitiveValue>? a,
			IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) <= 0;
		}

		public static bool operator <=(IStrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
		{
			return a?.PrimitiveValue.CompareTo(b) <= 0;
		}

		public static bool operator <=(TPrimitiveValue? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.CompareTo(b is null ? null : b.PrimitiveValue) <= 0;
		}

		public static bool operator !=(IStrongTypedValue<TSelf, TPrimitiveValue>? a,
			IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			if (a is null && b is null)
			{
				return false;
			}

			return a?.PrimitiveValue.Equals(b is null ? null : b.PrimitiveValue) != true;
		}

		public static bool operator !=(IStrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
		{
			return a?.PrimitiveValue.Equals(b) != true;
		}

		public static bool operator !=(TPrimitiveValue? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
		{
			return a?.Equals(b is null ? null : b.PrimitiveValue) != true;
		}
	}


	extension<TSelf, TPrimitiveId>(IStrongTypedId<TSelf, TPrimitiveId>)
		where TSelf : IStrongTypedId<TSelf, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
		IParsable<TPrimitiveId>
	{
		public static TSelf Parse(string s, IFormatProvider? provider = null)
		{
			return IStrongTypedValue<TSelf, TPrimitiveId>.Create(TPrimitiveId.Parse(s, provider));
		}

		public static bool TryParse(string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
		{
			if (TPrimitiveId.TryParse(s, provider, out var primitiveId))
			{
				result = IStrongTypedValue<TSelf, TPrimitiveId>.Create(primitiveId);
				return true;
			}

			result = default;
			return false;
		}

		public static bool TryParse(string? s, [MaybeNullWhen(false)] out TSelf result)
		{
			return IStrongTypedId<TSelf, TPrimitiveId>.TryParse<TSelf, TPrimitiveId>(s, null, out result);
		}
	}

	extension<TSelf>(IStrongTypedGuid<TSelf>)
		where TSelf : IStrongTypedGuid<TSelf>
	{
		public static TSelf Empty => IStrongTypedValue<TSelf, Guid>.Create(Guid.Empty);

		/// <Summary>
		///     Creates a new instance of your strong typed id with Guid.CreateVersion7() as its primitive id.
		/// </Summary>
		public static TSelf New()
		{
			return IStrongTypedValue<TSelf, Guid>.Create(Guid.CreateVersion7());
		}
	}

	extension<TSelf, TEnum>(IStrongTypedEnumValue<TSelf, TEnum> value)
		where TSelf : IStrongTypedEnumValue<TSelf, TEnum>
		where TEnum : struct, Enum
	{
		public TEnum PrimitiveEnumValue => Enum.Parse<TEnum>(value.PrimitiveValue, true);

		public static IEnumerable<TSelf> GetValidValues()
		{
			return Enum.GetValues<TEnum>().Select(value => IStrongTypedValue<TSelf, string>.Create(value.ToString()));
		}
	}
}