namespace StrongTypedId.IntegrationTests;

[Collection(nameof(ConfigurationCollection))]
public class MongoDbSerializationTests : BaseTests
{
	private readonly IRepository<FakeAggregate, FakeId> _repository;

	public MongoDbSerializationTests(ContainerFixture fixture) : base(fixture)
	{
		_repository = Provider.GetRequiredService<IRepository<FakeAggregate, FakeId>>();
	}

	[Fact]
	public async Task Insert_StrongTypedGuid_IsInserted()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New());

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.Equal(fake, fetched);
	}
}