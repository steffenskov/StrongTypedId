namespace StrongTypedId;

public interface IStrongTypedValue<TPrimitiveValue> : IStrongTypedValue, IComparable, IComparable<TPrimitiveValue>
	where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
	new TPrimitiveValue PrimitiveValue { get; }


	int IComparable.CompareTo(object? obj)
	{
		return PrimitiveValue.CompareTo(obj);
	}

	int IComparable<TPrimitiveValue>.CompareTo(TPrimitiveValue? other)
	{
		return PrimitiveValue.CompareTo(other);
	}

	object? IStrongTypedValue.PrimitiveValue => PrimitiveValue;
}

/// <summary>
///     Marker interface for all strong typed values
/// </summary>
public interface IStrongTypedValue
{
	object? PrimitiveValue { get; }
}

public interface IStrongTypedValue<TSelf, TPrimitiveValue> : IStrongTypedValue<TPrimitiveValue>, IComparable<IStrongTypedValue<TSelf, TPrimitiveValue>>,
	IEquatable<TSelf>
	where TSelf : IStrongTypedValue<TSelf, TPrimitiveValue>
	where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
	int IComparable<IStrongTypedValue<TSelf, TPrimitiveValue>>.CompareTo(IStrongTypedValue<TSelf, TPrimitiveValue>? other)
	{
		return PrimitiveValue.CompareTo(other is null ? null : other.PrimitiveValue);
	}

	bool IEquatable<TSelf>.Equals(TSelf? other)
	{
		return PrimitiveValue.Equals(other is null ? null : other.PrimitiveValue);
	}
}