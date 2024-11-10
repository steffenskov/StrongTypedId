namespace StrongTypedId.UnitTests;

public class StrongTypedEnumTests
{
	[Fact]
	public void Create_InvalidString_Throws()
	{
		// Arrange
		var primitive = "Hello world";

		// Act && Assert
		Assert.Throws<ArgumentException>(() => FakeStrongEnum.Create(primitive));
	}

	[Fact]
	public void Create_ValidString_Created()
	{
		// Arrange
		var primitive = nameof(FakeEnum.Value1);

		// Act
		var strongEnum = FakeStrongEnum.Create(primitive);

		// Assert
		Assert.Equal(FakeEnum.Value1, strongEnum.PrimitiveEnumValue);
	}

	[Fact]
	public void Ctor_InvalidEnum_Throws()
	{
		// Arrange
		FakeEnum primitive = 0;

		// Act && Assert
		Assert.Throws<ArgumentException>(() => new FakeStrongEnum(primitive));
	}

	[Fact]
	public void Ctor_InvalidString_Throws()
	{
		// Arrange
		var primitive = "Hello world";

		// Act && Assert
		Assert.Throws<ArgumentException>(() => new FakeStrongEnum(primitive));
	}

	[Fact]
	public void Ctor_ValidEnum_Created()
	{
		// Arrange
		var primitive = FakeEnum.Value1;

		// Act
		var strongEnum = new FakeStrongEnum(primitive);

		// Assert
		Assert.Equal(primitive, strongEnum.PrimitiveEnumValue);
	}

	[Fact]
	public void Ctor_ValidString_Created()
	{
		// Arrange
		var primitive = nameof(FakeEnum.Value1);

		// Act
		var strongEnum = new FakeStrongEnum(primitive);

		// Assert
		Assert.Equal(FakeEnum.Value1, strongEnum.PrimitiveEnumValue);
	}
}

file class FakeStrongEnum : StrongTypedEnum<FakeStrongEnum, FakeEnum>
{
	public FakeStrongEnum(FakeEnum value) : base(value)
	{
	}

	public FakeStrongEnum(string value) : base(value)
	{
	}
}

file enum FakeEnum
{
	Value1 = 1,
	Value2 = 2
}