using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

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

		services.AddSingleton<IRepository<FakeAggregate, FakeId>>(new Repository<FakeAggregate, FakeId>(db, "fakes"));
		Provider = services.BuildServiceProvider();
	}

	protected ServiceProvider Provider { get; }
}