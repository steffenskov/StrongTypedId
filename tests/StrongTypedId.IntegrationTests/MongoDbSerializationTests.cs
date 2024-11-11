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
	public async Task MongoSerialization_Guid_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New());

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.Equal(fake, fetched);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task MongoSerialization_BoolId_HandlesSerialization(bool primitive)
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			BoolId = new StrongBoolId(primitive)
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(primitive, fetched.BoolId.PrimitiveValue);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task MongoSerialization_BoolValue_HandlesSerialization(bool primitive)
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			BoolValue = new StrongBoolValue(primitive)
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(primitive, fetched.BoolValue.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_DecimalId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			DecimalId = new StrongDecimalId((decimal)Random.Shared.NextDouble())
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.DecimalId.PrimitiveValue, fetched.DecimalId.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_DecimalValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			DecimalValue = new StrongDecimalValue((decimal)Random.Shared.NextDouble())
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.DecimalValue.PrimitiveValue, fetched.DecimalValue.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_DoubleId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			DoubleId = new StrongDoubleId(Random.Shared.NextDouble())
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.DoubleId.PrimitiveValue, fetched.DoubleId.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_DoubleValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			DoubleValue = new StrongDoubleValue(Random.Shared.NextDouble())
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.DoubleValue.PrimitiveValue, fetched.DoubleValue.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_EnumValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			EnumValue = new StrongEnumValue(FakeEnum.Value1)
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(FakeEnum.Value1, fetched.EnumValue.PrimitiveEnumValue);
	}

	[Fact]
	public async Task MongoSerialization_GuidId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			GuidId = new StrongGuidId(Guid.NewGuid())
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.GuidId.PrimitiveValue, fetched.GuidId.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_GuidValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			GuidValue = new StrongGuidValue(Guid.NewGuid())
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.GuidValue.PrimitiveValue, fetched.GuidValue.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_IntId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			IntId = new StrongIntId(Random.Shared.Next(int.MinValue, int.MaxValue))
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.IntId.PrimitiveValue, fetched.IntId.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_IntValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			IntValue = new StrongIntValue(Random.Shared.Next(int.MinValue, int.MaxValue))
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.IntValue.PrimitiveValue, fetched.IntValue.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_LongId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			LongId = new StrongLongId(Random.Shared.NextInt64(long.MinValue, long.MaxValue))
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.LongId.PrimitiveValue, fetched.LongId.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_LongValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			LongValue = new StrongLongValue(Random.Shared.NextInt64(long.MinValue, long.MaxValue))
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.LongValue.PrimitiveValue, fetched.LongValue.PrimitiveValue);
	}

	[Fact]
	public async Task MongoSerialization_StringValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			StringValue = new StrongStringValue("Hello world")
		};

		// Act
		await _repository.InsertAsync(fake);

		// Assert
		var fetched = await _repository.GetAsync(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.StringValue.PrimitiveValue, fetched.StringValue.PrimitiveValue);
	}
}