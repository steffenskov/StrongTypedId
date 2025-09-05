namespace StrongTypedId.LiteDB.Serializers;

internal class StrongTypedDoubleSerializer<TStrongTypedValue> : ILiteDBSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, double>
{
	public TStrongTypedValue? Deserialize(BsonValue value)
	{
		if (value.Type == BsonType.Null)
		{
			return null;
		}

		if (value.Type == BsonType.String)
		{
			return StrongTypedValue<TStrongTypedValue, double>.Create(double.Parse(value.AsString));
		}

		return StrongTypedValue<TStrongTypedValue, double>.Create(value.AsDouble);
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