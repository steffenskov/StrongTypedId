using StrongTypedId.Reflection;

namespace StrongTypedId;

public abstract class StrongTypedValue
{
	public static object Create(Type type, object primitiveValue)
	{
		return DynamicActivator.Create(type, primitiveValue);
	}
}

/// <Summary>
///     Abstract baseclass to represent a strong typed value. Use it like this:
///     public class EmailAddress: StrongTypedValue&lt;EmailAddress, string&gt;
/// </Summary>
public abstract class StrongTypedValue<TSelf, TPrimitiveValue> : StrongTypedValue, IComparable,
	IComparable<StrongTypedValue<TSelf, TPrimitiveValue>>, IComparable<TPrimitiveValue>,
	IEquatable<TSelf>, IStrongTypedValue<TPrimitiveValue>
	where TSelf : StrongTypedValue<TSelf, TPrimitiveValue>
	where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
	private static readonly DynamicActivator<TSelf, TPrimitiveValue> _dynamicActivator = new();


	protected StrongTypedValue(TPrimitiveValue primitiveValue)
	{
		PrimitiveValue = primitiveValue;
	}

	public int CompareTo(object? obj)
	{
		return PrimitiveValue.CompareTo(obj);
	}

	public int CompareTo(StrongTypedValue<TSelf, TPrimitiveValue>? other)
	{
		return PrimitiveValue.CompareTo(other is null ? null : other.PrimitiveValue);
	}

	public int CompareTo(TPrimitiveValue? other)
	{
		return PrimitiveValue.CompareTo(other);
	}

	public bool Equals(TSelf? other)
	{
		return PrimitiveValue.Equals(other is null ? null : other.PrimitiveValue);
	}

	public TPrimitiveValue PrimitiveValue { get; }

	public static TSelf Create(TPrimitiveValue value)
	{
		return _dynamicActivator.Create(value);
	}

	public override bool Equals(object? obj)
	{
		if (obj is TSelf strongTyped)
		{
			return PrimitiveValue.Equals(strongTyped.PrimitiveValue);
		}

		return PrimitiveValue.Equals(obj);
	}

	public override int GetHashCode()
	{
		return PrimitiveValue.GetHashCode();
	}

	public override string ToString()
	{
		return PrimitiveValue.ToString()!;
	}

	public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		if (a is null && b is null)
		{
			return true;
		}

		return a?.PrimitiveValue.Equals(b is null ? null : b.PrimitiveValue) == true;
	}

	public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
	{
		return a?.PrimitiveValue.Equals(b) == true;
	}

	public static bool operator ==(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.Equals(b is null ? null : b.PrimitiveValue) == true;
	}

	public static bool operator >(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) > 0;
	}

	public static bool operator >(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) > 0;
	}

	public static bool operator >(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) > 0;
	}

	public static bool operator <(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) < 0;
	}

	public static bool operator <(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) < 0;
	}

	public static bool operator <(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) < 0;
	}

	public static bool operator >=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) >= 0;
	}

	public static bool operator >=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) >= 0;
	}

	public static bool operator >=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) >= 0;
	}

	public static bool operator <=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) <= 0;
	}

	public static bool operator <=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) <= 0;
	}

	public static bool operator <=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) <= 0;
	}

	public static bool operator !=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		if (a is null && b is null)
		{
			return false;
		}

		return a?.PrimitiveValue.Equals(b is null ? null : b.PrimitiveValue) != true;
	}

	public static bool operator !=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
	{
		return a?.PrimitiveValue.Equals(b) != true;
	}

	public static bool operator !=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.Equals(b is null ? null : b.PrimitiveValue) != true;
	}
}