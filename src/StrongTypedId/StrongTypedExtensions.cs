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


	extension<TSelf, TPrimitiveId>(StrongTypedId<TSelf, TPrimitiveId>)
		where TSelf : StrongTypedId<TSelf, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
		IParsable<TPrimitiveId>
	{
		public static TSelf Parse(string s, IFormatProvider? provider = null)
		{
			return Create<TSelf, TPrimitiveId>(TPrimitiveId.Parse(s, provider));
		}

		public static bool TryParse(string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
		{
			if (TPrimitiveId.TryParse(s, provider, out var primitiveId))
			{
				result = Create<TSelf, TPrimitiveId>(primitiveId);
				return true;
			}

			result = null;
			return false;
		}

		public static bool TryParse(string? s, [MaybeNullWhen(false)] out TSelf result)
		{
			return TryParse<TSelf, TPrimitiveId>(s, null, out result);
		}
	}

	extension<TSelf>(IStrongTypedGuid<TSelf>)
		where TSelf : IStrongTypedGuid<TSelf>
	{
		public static TSelf Empty => Create<TSelf, Guid>(Guid.Empty);

		/// <Summary>
		///     Creates a new instance of your strong typed id with Guid.CreateVersion7() as its primitive id.
		/// </Summary>
		public static TSelf New()
		{
			return Create<TSelf, Guid>(Guid.CreateVersion7());
		}
	}
}