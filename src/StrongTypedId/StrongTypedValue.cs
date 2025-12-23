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