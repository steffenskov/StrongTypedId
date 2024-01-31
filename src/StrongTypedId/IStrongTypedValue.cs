namespace StrongTypedId;

public interface IStrongTypedValue<out TPrimitiveValue>:IStrongTypedValue
{
	TPrimitiveValue PrimitiveValue { get; }
}

/// <summary>
/// Marker interface for all strong typed values
/// </summary>
public interface IStrongTypedValue
{
}