namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedIntSerializer<TStrongTypedValue> : ILiteDBSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, int>
{
	public TStrongTypedValue? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return null;
		}

		if (value.Type == BsonType.String)
		{
			return StrongTypedValue<TStrongTypedValue, int>.Create(int.Parse(value.AsString));
		}

		return StrongTypedValue<TStrongTypedValue, int>.Create(value.AsInt32);
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