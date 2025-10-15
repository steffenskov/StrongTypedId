namespace StrongTypedId.LiteDB.Serializers.Factories;

internal interface ILiteDBSerializerFactory
{
	bool CanSerialize(Type type);
	ILiteDBSerializer CreateSerializer(Type type);
}