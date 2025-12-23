namespace StrongTypedId;

public interface IStrongTypedGuid : IStrongTypedId<Guid>
{
}

public interface IStrongTypedGuid<TSelf> : IStrongTypedGuid, IStrongTypedId<TSelf, Guid>
	where TSelf : IStrongTypedId<TSelf, Guid>
{
}