namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedGuidSerializer<TStrongTypedId> : ILiteDBSerializer<TStrongTypedId?>
	where TStrongTypedId : StrongTypedGuid<TStrongTypedId>
{
	public TStrongTypedId? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return null;
		}

		if (value.Type == BsonType.String)
		{
			return StrongTypedGuid<TStrongTypedId>.Parse(value.AsString);
		}

		return StrongTypedGuid<TStrongTypedId>.Create(value.AsGuid);
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