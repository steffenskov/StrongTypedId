namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedLongSerializer<TStrongTypedValue> : ILiteDBSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, long>
{
	public TStrongTypedValue? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return null;
		}

		if (value.Type == BsonType.String)
		{
			return StrongTypedExtensions.Create<TStrongTypedValue, long>(long.Parse(value.AsString));
		}

		return StrongTypedExtensions.Create<TStrongTypedValue, long>(value.AsInt64);
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