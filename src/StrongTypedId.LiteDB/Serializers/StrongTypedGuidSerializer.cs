namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedGuidSerializer<TStrongTypedId> : ILiteDBSerializer<TStrongTypedId?>
	where TStrongTypedId : IStrongTypedGuid<TStrongTypedId>
{
	public TStrongTypedId? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return default;
		}

		if (value.Type == BsonType.String)
		{
			return StrongTypedExtensions.Parse<TStrongTypedId, Guid>(value.AsString);
		}

		return StrongTypedExtensions.Create<TStrongTypedId, Guid>(value.AsGuid);
	}

	public BsonValue Serialize(TStrongTypedId? arg)
	{
		if (arg is null)
		{
			return BsonValue.Null;
		}

		return arg.PrimitiveValue;
	}
}