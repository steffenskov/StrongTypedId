using LiteDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using StrongTypedId.LiteDB;
using BsonSerializer = MongoDB.Bson.Serialization.BsonSerializer;

namespace StrongTypedId.IntegrationTests;

public abstract class BaseTests
{
	static BaseTests()
	{
		BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
	}

	protected BaseTests(ContainerFixture fixture)
	{
		var services = new ServiceCollection();
		var client = new MongoClient(fixture.MongoConnectionString);
		var db = client.GetDatabase("test");
		var mapper = StrongTypedLiteDB.CreateBsonMapper(typeof(FakeId).Assembly);
		var liteDb = new LiteDatabase(":memory:", mapper);

		services.AddSingleton<IMongoRepository<FakeAggregate, FakeId>>(new MongoRepository<FakeAggregate, FakeId>(db, "fakes"));
		services.AddSingleton<ILiteDBRepository<FakeAggregate, FakeId>>(new LiteDBRepository<FakeAggregate, FakeId>(liteDb, "fakes"));
		Provider = services.BuildServiceProvider();
	}

	protected ServiceProvider Provider { get; }
}