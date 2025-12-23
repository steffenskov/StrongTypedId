namespace StrongTypedId.MongoDB.Serializers;

internal class StrongTypedDecimalSerializer<TStrongTypedValue> : IBsonSerializer<TStrongTypedValue?>
	where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, decimal>
{
	public Type ValueType { get; } = typeof(TStrongTypedValue);

	public TStrongTypedValue? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
	{
		if (context.Reader.CurrentBsonType == BsonType.Null)
		{
			context.Reader.ReadNull(); // Important, Mongo completely breaks without this!
			return null;
		}

		decimal rawValue;

		if (context.Reader.CurrentBsonType == BsonType.Document)
		{
			context.Reader.ReadStartDocument();
			context.Reader.ReadString(); // reads typename
			rawValue = (decimal)context.Reader.ReadDecimal128();
			context.Reader.ReadEndDocument();
		}
		else
		{
			rawValue = (decimal)context.Reader.ReadDecimal128();
		}

		return StrongTypedExtensions.Create<TStrongTypedValue, decimal>(rawValue);
	}

	public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TStrongTypedValue? value)
	{
		if (value is null)
		{
			context.Writer.WriteNull();
		}
		else
		{
			context.Writer.WriteDecimal128(value.PrimitiveValue);
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