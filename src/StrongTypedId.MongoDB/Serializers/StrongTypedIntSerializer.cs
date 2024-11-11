namespace StrongTypedId.MongoDB.Serializers;

internal class StrongTypedIntSerializer<TStrongTypedValue> : IBsonSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, int>
{
	public Type ValueType { get; } = typeof(TStrongTypedValue);

	public TStrongTypedValue? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
	{
		if (context.Reader.CurrentBsonType == BsonType.Null)
		{
			context.Reader.ReadNull(); // Important, Mongo completely breaks without this!
			return null;
		}

		int rawValue;

		if (context.Reader.CurrentBsonType == BsonType.Document)
		{
			context.Reader.ReadStartDocument();
			context.Reader.ReadString(); // reads typename
			rawValue = context.Reader.ReadInt32();
			context.Reader.ReadEndDocument();
		}
		else
		{
			rawValue = context.Reader.ReadInt32();
		}

		return StrongTypedValue<TStrongTypedValue, int>.Create(rawValue);
	}

	public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TStrongTypedValue? value)
	{
		if (value is null)
		{
			context.Writer.WriteNull();
		}
		else
		{
			context.Writer.WriteInt32(value.PrimitiveValue);
		}
	}

	public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
	{
		Serialize(context, args, (TStrongTypedValue)value);
	}

	object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
	{
		return Deserialize(context, args);
	}
}