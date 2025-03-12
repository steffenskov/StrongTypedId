namespace StrongTypedId.UnitTests.Operators;

public class LargerThanOrEqualOperatorTests
{
	[Fact]
	public void LargerThanOrEqualOperator_BothAreStrongTypedAndOtherIsLarger_IsLargerOrEqual()
	{
		// Arrange
		var idOne = new IntId(42);
		var idTwo = new IntId(1337);

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.True(isLarger);
	}

	[Fact]
	public void LargerThanOrEqualOperator_BothAreStrongTypedAndOtherIsNotLarger_IsNotLargerOrEqual()
	{
		// Arrange
		var idOne = new IntId(1337);
		var idTwo = new IntId(42);

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.False(isLarger);
	}

	[Fact]
	public void LargerThanOrEqualOperator_BothAreStrongTypedEqual_IsLargerOrEqual()
	{
		// Arrange
		var idOne = new IntId(42);
		var idTwo = new IntId(42);

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.True(isLarger);
	}

	[Fact]
	public void LargerThanOrEqualOperator_OneIsStrongTypedAndOtherIsLarger_IsLargerOrEqual()
	{
		// Arrange
		var idOne = new IntId(42);
		var idTwo = 1337;

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.True(isLarger);
	}

	[Fact]
	public void LargerThanOrEqualOperator_OneIsStrongTypedAndOtherIsNotLarger_IsNotLargerOrEqual()
	{
		// Arrange
		var idOne = new IntId(1337);
		var idTwo = 42;

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.False(isLarger);
	}

	[Fact]
	public void LargerThanOrEqualOperator_OneIsStrongTypedEqual_IsLargerOrEqual()
	{
		// Arrange
		var idOne = new IntId(42);
		var idTwo = 42;

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.True(isLarger);
	}


	[Fact]
	public void LargerThanOrEqualOperator_OtherIsStrongTypedAndOtherIsLarger_IsLargerOrEqual()
	{
		// Arrange
		var idOne = 42;
		var idTwo = new IntId(1337);

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.True(isLarger);
	}

	[Fact]
	public void LargerThanOrEqualOperator_OtherIsStrongTypedAndOtherIsNotLarger_IsNotLargerOrEqual()
	{
		// Arrange
		var idOne = 1337;
		var idTwo = new IntId(42);

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.False(isLarger);
	}

	[Fact]
	public void LargerThanOrEqualOperator_OtherIsStrongTypedEqual_IsLargerOrEqual()
	{
		// Arrange
		var idOne = 42;
		var idTwo = new IntId(42);

		// Act
		var isLarger = idTwo >= idOne;

		// Assert
		Assert.True(isLarger);
	}
}