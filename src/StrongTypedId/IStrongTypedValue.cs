namespace StrongTypedId;

public interface IStrongTypedValue<out TPrimitiveValue> : IStrongTypedValue
{
	new TPrimitiveValue PrimitiveValue { get; }

	object? IStrongTypedValue.PrimitiveValue => PrimitiveValue;
}

/// <summary>
///     Marker interface for all strong typed values
/// </summary>
public interface IStrongTypedValue
{
	object? PrimitiveValue { get; }
}