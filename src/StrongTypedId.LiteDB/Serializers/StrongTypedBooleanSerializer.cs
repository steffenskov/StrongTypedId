namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedBooleanSerializer<TStrongTypedValue> : ILiteDBSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, bool>
{
	public TStrongTypedValue? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return null;
		}

		if (value.Type == BsonType.String)
		{
			return StrongTypedValue<TStrongTypedValue, bool>.Create(bool.Parse(value.AsString));
		}

		return StrongTypedValue<TStrongTypedValue, bool>.Create(value.AsBoolean);
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