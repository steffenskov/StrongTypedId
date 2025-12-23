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

	extension<TSelf, TPrimitiveValue>(TSelf)
		where TSelf : IStrongTypedValue<TSelf, TPrimitiveValue>
		where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
	{
		public static TSelf Create(TPrimitiveValue value)
		{
			return DynamicActivator.GetActivator<TSelf, TPrimitiveValue>().Create(value); // TODO: Suffers performance hit b/c of double dictionary lookup, consider different design
		}
	}


	extension<TSelf, TPrimitiveId>(IStrongTypedId<TSelf, TPrimitiveId>)
		where TSelf : IStrongTypedId<TSelf, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
		IParsable<TPrimitiveId>
	{
		public static TSelf Parse(string s, IFormatProvider? provider = null)
		{
			return Create<TSelf, TPrimitiveId>(TPrimitiveId.Parse(s, provider));
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public static bool TryParse(string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
		{
			if (TPrimitiveId.TryParse(s, provider, out var primitiveId))
			{
				result = Create<TSelf, TPrimitiveId>(primitiveId);
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
		public static TSelf Empty => Create<TSelf, Guid>(Guid.Empty);

		/// <Summary>
		///     Creates a new instance of your strong typed id with Guid.CreateVersion7() as its primitive id.
		/// </Summary>
		public static TSelf New()
		{
			return Create<TSelf, Guid>(Guid.CreateVersion7());
		}
	}

	extension<TSelf, TEnum>(IStrongTypedEnumValue<TSelf, TEnum> value)
		where TSelf : IStrongTypedEnumValue<TSelf, TEnum>
		where TEnum : struct, Enum
	{
		public TEnum PrimitiveEnumValue => Enum.Parse<TEnum>(value.PrimitiveValue, true);

		public static IEnumerable<TSelf> GetValidValues()
		{
			return Enum.GetValues<TEnum>().Select(value => Create<TSelf, string>(value.ToString()));
		}
	}
}