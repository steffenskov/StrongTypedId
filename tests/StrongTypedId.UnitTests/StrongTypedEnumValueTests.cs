namespace StrongTypedId.UnitTests;

public class StrongTypedEnumValueTests
{
	[Fact]
	public void Create_InvalidString_Throws()
	{
		// Arrange
		var primitive = "Hello world";

		// Act && Assert
		Assert.Throws<ArgumentException>(() => FakeStrongEnumValue.Create(primitive));
	}

	[Fact]
	public void Create_ValidString_Created()
	{
		// Arrange
		var primitive = nameof(FakeEnum.Value1);

		// Act
		var strongEnum = FakeStrongEnumValue.Create(primitive);

		// Assert
		Assert.Equal(FakeEnum.Value1, strongEnum.PrimitiveEnumValue);
	}

	[Fact]
	public void Ctor_InvalidEnum_Throws()
	{
		// Arrange
		FakeEnum primitive = 0;

		// Act && Assert
		Assert.Throws<ArgumentException>(() => new FakeStrongEnumValue(primitive));
	}

	[Fact]
	public void Ctor_InvalidString_Throws()
	{
		// Arrange
		var primitive = "Hello world";

		// Act && Assert
		Assert.Throws<ArgumentException>(() => new FakeStrongEnumValue(primitive));
	}

	[Fact]
	public void Ctor_ValidEnum_Created()
	{
		// Arrange
		var primitive = FakeEnum.Value1;

		// Act
		var strongEnum = new FakeStrongEnumValue(primitive);

		// Assert
		Assert.Equal(primitive, strongEnum.PrimitiveEnumValue);
	}

	[Fact]
	public void Ctor_ValidString_Created()
	{
		// Arrange
		var primitive = nameof(FakeEnum.Value1);

		// Act
		var strongEnum = new FakeStrongEnumValue(primitive);

		// Assert
		Assert.Equal(FakeEnum.Value1, strongEnum.PrimitiveEnumValue);
	}
}

file class FakeStrongEnumValue : StrongTypedEnumValue<FakeStrongEnumValue, FakeEnum>
{
	public FakeStrongEnumValue(FakeEnum value) : base(value)
	{
	}

	public FakeStrongEnumValue(string value) : base(value)
	{
	}
}

file enum FakeEnum
{
	Value1 = 1,
	Value2 = 2
}