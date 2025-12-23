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
			return StrongTypedExtensions.Create<TStrongTypedValue, decimal>(decimal.Parse(value.AsString));
		}

		return StrongTypedExtensions.Create<TStrongTypedValue, decimal>(value.AsDecimal);
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