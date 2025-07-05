using DotNet.Testcontainers.Builders;
using StrongTypedId.MongoDB;
using Testcontainers.MongoDb;

namespace StrongTypedId.IntegrationTests.Fixtures;

public class ContainerFixture : IAsyncLifetime
{
	private readonly MongoDbContainer _mongoContainer;

	public ContainerFixture()
	{
		StrongTypedMongo.AddStrongTypedMongoSerializers(typeof(FakeId).Assembly);
		_mongoContainer = new MongoDbBuilder()
			.WithImage("mongo:latest")
			.WithUsername("mongo")
			.WithPassword("secret")
			.WithPortBinding(27017, 27017)
			.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
			.Build();
	}

	public string MongoConnectionString => _mongoContainer.GetConnectionString();

	public async ValueTask InitializeAsync()
	{
		await _mongoContainer.StartAsync();
	}

	public async ValueTask DisposeAsync()
	{
		await _mongoContainer.DisposeAsync();
	}
}