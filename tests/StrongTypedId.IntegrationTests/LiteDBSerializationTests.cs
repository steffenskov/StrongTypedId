namespace StrongTypedId.IntegrationTests;

[Collection(nameof(ConfigurationCollection))]
public class LiteDBSerializationTests : BaseTests
{
	private readonly ILiteDBRepository<FakeAggregate, FakeId> _repository;

	public LiteDBSerializationTests(ContainerFixture fixture) : base(fixture)
	{
		_repository = Provider.GetRequiredService<ILiteDBRepository<FakeAggregate, FakeId>>();
	}

	[Fact]
	public void LiteDBSerialization_Guid_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New());

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.Equal(fake, fetched);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public void LiteDBSerialization_BoolId_HandlesSerialization(bool primitive)
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			BoolId = new StrongBoolId(primitive)
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(primitive, fetched.BoolId.PrimitiveValue);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public void LiteDBSerialization_BoolValue_HandlesSerialization(bool primitive)
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			BoolValue = new StrongBoolValue(primitive)
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(primitive, fetched.BoolValue.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_DecimalId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			DecimalId = new StrongDecimalId((decimal)Random.Shared.NextDouble())
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.DecimalId.PrimitiveValue, fetched.DecimalId.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_DecimalValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			DecimalValue = new StrongDecimalValue((decimal)Random.Shared.NextDouble())
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.DecimalValue.PrimitiveValue, fetched.DecimalValue.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_DoubleId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			DoubleId = new StrongDoubleId(Random.Shared.NextDouble())
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.DoubleId.PrimitiveValue, fetched.DoubleId.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_DoubleValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			DoubleValue = new StrongDoubleValue(Random.Shared.NextDouble())
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.DoubleValue.PrimitiveValue, fetched.DoubleValue.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_EnumValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			EnumValue = new StrongEnumValue(FakeEnum.Value1)
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(FakeEnum.Value1, fetched.EnumValue.PrimitiveEnumValue);
	}

	[Fact]
	public void LiteDBSerialization_GuidId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			GuidId = new StrongGuidId(Guid.NewGuid())
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.GuidId.PrimitiveValue, fetched.GuidId.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_GuidValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			GuidValue = new StrongGuidValue(Guid.NewGuid())
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.GuidValue.PrimitiveValue, fetched.GuidValue.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_IntId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			IntId = new StrongIntId(Random.Shared.Next(int.MinValue, int.MaxValue))
		};

		// Act
		_repository.Insert(fake);

		// Assert
		//	var fetched = _repository.GetSingle(fake.Id);
		var all = _repository.GetAll().ToList();

		//Assert.NotNull(fetched);
		//Assert.Equal(fake.IntId.PrimitiveValue, fetched.IntId.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_IntValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			IntValue = new StrongIntValue(Random.Shared.Next(int.MinValue, int.MaxValue))
		};

		// Act
		_repository.Insert(fake);

		// Assert
		//	var fetched = _repository.GetSingle(fake.Id);

		//Assert.NotNull(fetched);
		//Assert.Equal(fake.IntValue.PrimitiveValue, fetched.IntValue.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_AllProperties_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			BoolId = new StrongBoolId(true),
			BoolValue = new StrongBoolValue(true),
			DecimalId = new StrongDecimalId(Random.Shared.Next(int.MinValue, int.MaxValue)),
			DecimalValue = new StrongDecimalValue(Random.Shared.Next(int.MinValue, int.MaxValue)),
			DoubleId = new StrongDoubleId(Random.Shared.Next(int.MinValue, int.MaxValue)),
			DoubleValue = new StrongDoubleValue(Random.Shared.Next(int.MinValue, int.MaxValue)),
			GuidId = new StrongGuidId(Guid.NewGuid()),
			GuidValue = new StrongGuidValue(Guid.NewGuid()),
			IntId = new StrongIntId(Random.Shared.Next(int.MinValue, int.MaxValue)),
			IntValue = new StrongIntValue(Random.Shared.Next(int.MinValue, int.MaxValue)),
			LongId = new StrongLongId(Random.Shared.Next(int.MinValue, int.MaxValue)),
			LongValue = new StrongLongValue(Random.Shared.Next(int.MinValue, int.MaxValue)),
			StringValue = new StrongStringValue("Hello world"),
			EnumValue = new StrongEnumValue(FakeEnum.Value1)
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
	}

	[Fact]
	public void LiteDBSerialization_LongId_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			LongId = new StrongLongId(Random.Shared.NextInt64(long.MinValue, long.MaxValue))
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.LongId.PrimitiveValue, fetched.LongId.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_LongValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			LongValue = new StrongLongValue(Random.Shared.NextInt64(long.MinValue, long.MaxValue))
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.LongValue.PrimitiveValue, fetched.LongValue.PrimitiveValue);
	}

	[Fact]
	public void LiteDBSerialization_StringValue_HandlesSerialization()
	{
		// Arrange
		var fake = new FakeAggregate(FakeId.New())
		{
			StringValue = new StrongStringValue("Hello world")
		};

		// Act
		_repository.Insert(fake);

		// Assert
		var fetched = _repository.GetSingle(fake.Id);

		Assert.NotNull(fetched);
		Assert.Equal(fake.StringValue.PrimitiveValue, fetched.StringValue.PrimitiveValue);
	}
}