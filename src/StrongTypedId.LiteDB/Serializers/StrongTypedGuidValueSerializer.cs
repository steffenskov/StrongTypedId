namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedGuidValueSerializer<TStrongTypedValue> : ILiteDBSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, Guid>
{
	public TStrongTypedValue? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return null;
		}

		return StrongTypedValue<TStrongTypedValue, Guid>.Create(value.AsGuid);
	}

	public BsonValue Serialize(TStrongTypedValue? arg)
	{
		if (arg is null)
		{
			return BsonValue.Null;
		}

		return arg.PrimitiveValue;
	}
}