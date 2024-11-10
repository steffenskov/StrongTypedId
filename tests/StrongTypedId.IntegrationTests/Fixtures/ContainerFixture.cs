using StrongTypedId.MongoDB;
using Testcontainers.MongoDb;

namespace StrongTypedId.IntegrationTests.Fixtures;

public class ContainerFixture : IAsyncLifetime
{
	private readonly MongoDbContainer _mongoContainer;

	public ContainerFixture()
	{
		StrongTypedMongo.AddStrongTypedMongoSerializers(typeof(FakeId));
		_mongoContainer = new MongoDbBuilder()
			.WithImage("mongo:latest")
			.WithUsername("mongo")
			.WithPassword("secret")
			.Build();
	}

	public string MongoConnectionString => _mongoContainer.GetConnectionString();

	public async Task InitializeAsync()
	{
		await _mongoContainer.StartAsync();
	}

	public async Task DisposeAsync()
	{
		await _mongoContainer.DisposeAsync();
	}
}