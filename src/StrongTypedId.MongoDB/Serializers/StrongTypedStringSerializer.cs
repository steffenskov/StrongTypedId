namespace StrongTypedId.MongoDB.Serializers;

internal class StrongTypedStringSerializer<TStrongTyped> : IBsonSerializer<TStrongTyped?>
	where TStrongTyped : StrongTypedValue<TStrongTyped, string>
{
	public Type ValueType { get; } = typeof(TStrongTyped);

	public TStrongTyped? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
	{
		if (context.Reader.CurrentBsonType == BsonType.Null)
		{
			context.Reader.ReadNull(); // Important, Mongo completely breaks without this!
			return null;
		}

		string rawValue;

		if (context.Reader.CurrentBsonType == BsonType.Document)
		{
			context.Reader.ReadStartDocument();
			context.Reader.ReadString(); // reads typename
			rawValue = context.Reader.ReadString();
			context.Reader.ReadEndDocument();
		}
		else
		{
			rawValue = context.Reader.ReadString();
		}

		return StrongTypedValue<TStrongTyped, string>.Create(rawValue);
	}

	public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TStrongTyped? value)
	{
		if (value is null)
		{
			context.Writer.WriteNull();
		}
		else
		{
			context.Writer.WriteString(value.PrimitiveValue);
		}
	}

	public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
	{
		Serialize(context, args, (TStrongTyped)value);
	}

	object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
	{
		return Deserialize(context, args);
	}
}