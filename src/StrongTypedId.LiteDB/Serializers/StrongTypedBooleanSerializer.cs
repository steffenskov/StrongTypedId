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
			return StrongTypedExtensions.Create<TStrongTypedValue, bool>(bool.Parse(value.AsString));
		}

		return StrongTypedExtensions.Create<TStrongTypedValue, bool>(value.AsBoolean);
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