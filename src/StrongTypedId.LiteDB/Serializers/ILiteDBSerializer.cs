namespace StrongTypedId.LiteDB.Serializers;

public interface ILiteDBSerializer
{
	BsonValue Serialize(object arg);
	object? Deserialize(BsonValue arg);
}

public interface ILiteDBSerializer<TStrongTypedValue> : ILiteDBSerializer
{
	BsonValue ILiteDBSerializer.Serialize(object arg)
	{
		return Serialize((TStrongTypedValue)arg);
	}

	object? ILiteDBSerializer.Deserialize(BsonValue arg)
	{
		return Deserialize(arg);
	}

	BsonValue Serialize(TStrongTypedValue arg);
	new TStrongTypedValue Deserialize(BsonValue arg);
}