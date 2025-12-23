namespace StrongTypedId.MongoDB.Serializers;

internal class StrongTypedGuidSerializer<TStrongTypedId> : IBsonSerializer<TStrongTypedId?>
	where TStrongTypedId : StrongTypedGuid<TStrongTypedId>
{
	public Type ValueType { get; } = typeof(TStrongTypedId);

	public TStrongTypedId? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
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
			context.Reader.ReadString(); // reads typename which mongo implicitly serialized
			rawValue = context.Reader.ReadString();
			context.Reader.ReadEndDocument();
		}
		else
		{
			rawValue = context.Reader.ReadString();
		}

		return StrongTypedExtensions.Parse<TStrongTypedId, Guid>(rawValue);
	}

	public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TStrongTypedId? value)
	{
		if (value is null)
		{
			context.Writer.WriteNull();
		}
		else
		{
			context.Writer.WriteString(value.PrimitiveValue.ToString());
		}
	}

	public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
	{
		Serialize(context, args, (TStrongTypedId)value);
	}

	object? IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
	{
		return Deserialize(context, args);
	}
}