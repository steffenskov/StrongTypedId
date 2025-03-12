namespace StrongTypedId.UnitTests;

public class ExtensionTests
{
	[Theory]
	[InlineData(42)]
	[InlineData("Hello world")]
	[InlineData(13.37)]
	public void IsStrongTypedValue_OtherType_ReturnsFalse(object obj)
	{
		// Act
		var isStrongTyped = obj.IsStrongTypedValue();

		// Assert
		Assert.False(isStrongTyped);
	}

	[Fact]
	public void IsStrongTypedValue_IsStrongTypedValue_ReturnsTrue()
	{
		// Arrange
		var obj = new AttributedEmailAddress("");

		// Act
		var isStrongTyped = obj.IsStrongTypedValue();

		// Assert
		Assert.True(isStrongTyped);
	}

	[Fact]
	public void IsStrongTypedValue_IsStrongTypedId_ReturnsTrue()
	{
		// Arrange
		var obj = new AttributedIntId(42);

		// Act
		var isStrongTyped = obj.IsStrongTypedValue();

		// Assert
		Assert.True(isStrongTyped);
	}

	[Fact]
	public void IsStrongTypedValue_IsStrongTypedGuid_ReturnsTrue()
	{
		// Arrange
		var obj = AttributedGuidId.New();

		// Act
		var isStrongTyped = obj.IsStrongTypedValue();

		// Assert
		Assert.True(isStrongTyped);
	}


	[Fact]
	public void IsStrongTypedValue_IsStrongTypedWithoutTypeInformation_ReturnsTrue()
	{
		// Arrange
		var obj = (object)AttributedGuidId.New();

		// Act
		var isStrongTyped = obj.IsStrongTypedValue();

		// Assert
		Assert.True(isStrongTyped);
	}
}