namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedDecimalSerializer<TStrongTypedValue> : ILiteDBSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, decimal>
{
	public TStrongTypedValue? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return null;
		}

		if (value.Type == BsonType.String)
		{
			return StrongTypedValue<TStrongTypedValue, decimal>.Create(decimal.Parse(value.AsString));
		}

		return StrongTypedValue<TStrongTypedValue, decimal>.Create(value.AsDecimal);
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