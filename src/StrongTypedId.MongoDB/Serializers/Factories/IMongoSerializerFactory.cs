namespace StrongTypedId.MongoDB.Serializers.Factories;

internal interface IMongoSerializerFactory
{
	bool CanSerialize(Type type);
	IBsonSerializer CreateSerializer(Type type);
}