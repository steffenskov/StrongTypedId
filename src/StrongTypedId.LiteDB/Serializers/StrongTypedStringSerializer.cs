namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedStringSerializer<TStrongTypedValue> : ILiteDBSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, string>
{
	public TStrongTypedValue? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return null;
		}

		return StrongTypedValue<TStrongTypedValue, string>.Create(value.AsString);
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