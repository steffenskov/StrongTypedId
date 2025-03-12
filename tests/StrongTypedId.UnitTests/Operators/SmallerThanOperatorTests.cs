namespace StrongTypedId.UnitTests.Operators;

public class SmallerThanOperatorTests
{
	[Fact]
	public void SmallerThanOperator_BothAreStrongTypedAndOtherIsSmaller_IsSmaller()
	{
		// Arrange
		var idOne = new AttributedIntId(1337);
		var idTwo = new AttributedIntId(42);

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.True(isSmaller);
	}

	[Fact]
	public void SmallerThanOperator_BothAreStrongTypedAndOtherIsNotSmaller_IsNotSmaller()
	{
		// Arrange
		var idOne = new AttributedIntId(42);
		var idTwo = new AttributedIntId(1337);

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.False(isSmaller);
	}

	[Fact]
	public void SmallerThanOperator_BothAreStrongTypedEqual_IsNotSmaller()
	{
		// Arrange
		var idOne = new AttributedIntId(42);
		var idTwo = new AttributedIntId(42);

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.False(isSmaller);
	}

	[Fact]
	public void SmallerThanOperator_OneIsStrongTypedAndOtherIsSmaller_IsSmaller()
	{
		// Arrange
		var idOne = new AttributedIntId(1337);
		var idTwo = 42;

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.True(isSmaller);
	}

	[Fact]
	public void SmallerThanOperator_OneIsStrongTypedAndOtherIsNotSmaller_IsNotSmaller()
	{
		// Arrange
		var idOne = new AttributedIntId(42);
		var idTwo = 1337;

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.False(isSmaller);
	}

	[Fact]
	public void SmallerThanOperator_OneIsStrongTypedEqual_IsNotSmaller()
	{
		// Arrange
		var idOne = new AttributedIntId(42);
		var idTwo = 42;

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.False(isSmaller);
	}


	[Fact]
	public void SmallerThanOperator_OtherIsStrongTypedAndOtherIsSmaller_IsSmaller()
	{
		// Arrange
		var idOne = 1337;
		var idTwo = new AttributedIntId(42);

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.True(isSmaller);
	}

	[Fact]
	public void SmallerThanOperator_OtherIsStrongTypedAndOtherIsNotSmaller_IsNotSmaller()
	{
		// Arrange
		var idOne = 42;
		var idTwo = new AttributedIntId(1337);

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.False(isSmaller);
	}

	[Fact]
	public void SmallerThanOperator_OtherIsStrongTypedEqual_IsNotSmaller()
	{
		// Arrange
		var idOne = 42;
		var idTwo = new AttributedIntId(42);

		// Act
		var isSmaller = idTwo < idOne;

		// Assert
		Assert.False(isSmaller);
	}
}