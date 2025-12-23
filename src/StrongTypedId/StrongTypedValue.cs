namespace StrongTypedId;

public abstract class StrongTypedValue
{
}

/// <Summary>
///     Abstract baseclass to represent a strong typed value. Use it like this:
///     public class EmailAddress: StrongTypedValue&lt;EmailAddress, string&gt;
/// </Summary>
public abstract class StrongTypedValue<TSelf, TPrimitiveValue> : StrongTypedValue,
	IStrongTypedValue<TSelf, TPrimitiveValue>
	where TSelf : StrongTypedValue<TSelf, TPrimitiveValue>
	where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
	protected StrongTypedValue(TPrimitiveValue primitiveValue)
	{
		PrimitiveValue = primitiveValue;
	}


	public TPrimitiveValue PrimitiveValue { get; }

	public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		if (a is null && b is null)
		{
			return true;
		}

		return a?.PrimitiveValue.Equals(b is null ? null : b.PrimitiveValue) == true;
	}

	public static bool operator ==(TSelf? a, TPrimitiveValue b)
	{
		return a?.PrimitiveValue.Equals(b) == true;
	}

	public static bool operator ==(TPrimitiveValue? a, TSelf? b)
	{
		return a?.Equals(b is null ? null : b.PrimitiveValue) == true;
	}

	public static bool operator >(TSelf? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) > 0;
	}

	public static bool operator >(TSelf? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) > 0;
	}

	public static bool operator >(TPrimitiveValue? a, TSelf? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) > 0;
	}

	public static bool operator <(TSelf? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) < 0;
	}

	public static bool operator <(TSelf? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) < 0;
	}

	public static bool operator <(TPrimitiveValue? a, TSelf? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) < 0;
	}

	public static bool operator >=(TSelf? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) >= 0;
	}

	public static bool operator >=(TSelf? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) >= 0;
	}

	public static bool operator >=(TPrimitiveValue? a, TSelf? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) >= 0;
	}

	public static bool operator <=(TSelf? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) <= 0;
	}

	public static bool operator <=(TSelf? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) <= 0;
	}

	public static bool operator <=(TPrimitiveValue? a, TSelf? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) <= 0;
	}

	public static bool operator !=(TSelf? a, IStrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		if (a is null && b is null)
		{
			return false;
		}

		return a?.PrimitiveValue.Equals(b is null ? null : b.PrimitiveValue) != true;
	}

	public static bool operator !=(TSelf? a, TPrimitiveValue b)
	{
		return a?.PrimitiveValue.Equals(b) != true;
	}

	public static bool operator !=(TPrimitiveValue? a, TSelf? b)
	{
		return a?.Equals(b is null ? null : b.PrimitiveValue) != true;
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
}