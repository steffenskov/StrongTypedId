namespace StrongTypedId.UnitTests.Operators;

public class SmallerThanOrEqualOperatorTests
{
	[Fact]
	public void SmallerThanOrEqualOperator_BothAreStrongTypedAndOtherIsSmaller_IsSmallerOrEqual()
	{
		// Arrange
		var idOne = new AttributedIntId(1337);
		var idTwo = new AttributedIntId(42);

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.True(isSmaller);
	}

	[Fact]
	public void SmallerThanOrEqualOperator_BothAreStrongTypedAndOtherIsNotSmaller_IsNotSmallerOrEqual()
	{
		// Arrange
		var idOne = new AttributedIntId(42);
		var idTwo = new AttributedIntId(1337);

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.False(isSmaller);
	}

	[Fact]
	public void SmallerThanOrEqualOperator_BothAreStrongTypedEqual_IsSmallerOrEqual()
	{
		// Arrange
		var idOne = new AttributedIntId(42);
		var idTwo = new AttributedIntId(42);

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.True(isSmaller);
	}

	[Fact]
	public void SmallerThanOrEqualOperator_OneIsStrongTypedAndOtherIsSmaller_IsSmallerOrEqual()
	{
		// Arrange
		var idOne = new AttributedIntId(1337);
		var idTwo = 42;

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.True(isSmaller);
	}

	[Fact]
	public void SmallerThanOrEqualOperator_OneIsStrongTypedAndOtherIsNotSmaller_IsNotSmallerOrEqual()
	{
		// Arrange
		var idOne = new AttributedIntId(42);
		var idTwo = 1337;

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.False(isSmaller);
	}

	[Fact]
	public void SmallerThanOrEqualOperator_OneIsStrongTypedEqual_IsSmallerOrEqual()
	{
		// Arrange
		var idOne = new AttributedIntId(42);
		var idTwo = 42;

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.True(isSmaller);
	}


	[Fact]
	public void SmallerThanOrEqualOperator_OtherIsStrongTypedAndOtherIsSmaller_IsSmallerOrEqual()
	{
		// Arrange
		var idOne = 1337;
		var idTwo = new AttributedIntId(42);

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.True(isSmaller);
	}

	[Fact]
	public void SmallerThanOrEqualOperator_OtherIsStrongTypedAndOtherIsNotSmaller_IsNotSmallerOrEqual()
	{
		// Arrange
		var idOne = 42;
		var idTwo = new AttributedIntId(1337);

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.False(isSmaller);
	}

	[Fact]
	public void SmallerThanOrEqualOperator_OtherIsStrongTypedEqual_IsSmallerOrEqual()
	{
		// Arrange
		var idOne = 42;
		var idTwo = new AttributedIntId(42);

		// Act
		var isSmaller = idTwo <= idOne;

		// Assert
		Assert.True(isSmaller);
	}
}