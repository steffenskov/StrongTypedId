namespace StrongTypedId;

public interface IStrongTypedId<out TPrimitive> : IStrongTypedValue<TPrimitive>, IStrongTypedId
{
}

public interface IStrongTypedId
{
}