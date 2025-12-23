namespace StrongTypedId;

public interface IStrongTypedId<TPrimitiveId> : IStrongTypedValue<TPrimitiveId>, IStrongTypedId
	where TPrimitiveId : IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>
{
}

public interface IStrongTypedId
{
}

public interface IStrongTypedId<TSelf, TPrimitiveId> : IStrongTypedId<TPrimitiveId>, IStrongTypedValue<TSelf, TPrimitiveId>, IParsable<TSelf?>
	where TPrimitiveId : IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>, IParsable<TPrimitiveId?>
	where TSelf : IStrongTypedId<TSelf, TPrimitiveId>, IParsable<TSelf?>
{
	static TSelf IParsable<TSelf?>.Parse(string s, IFormatProvider? provider)
	{
		return IStrongTypedValue<TSelf, TPrimitiveId>.Create(TPrimitiveId.Parse(s, provider)!);
	}

	static bool IParsable<TSelf?>.TryParse(string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
	{
		if (TPrimitiveId.TryParse(s, provider, out var primitiveId))
		{
			result = IStrongTypedValue<TSelf, TPrimitiveId>.Create(primitiveId!);
			return true;
		}

		result = default;
		return false;
	}
}