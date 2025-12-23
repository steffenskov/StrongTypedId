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
			return StrongTypedExtensions.Create<TStrongTypedValue, double>(double.Parse(value.AsString));
		}

		return StrongTypedExtensions.Create<TStrongTypedValue, double>(value.AsDouble);
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