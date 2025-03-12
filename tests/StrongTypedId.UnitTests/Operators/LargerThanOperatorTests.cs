namespace StrongTypedId.UnitTests.Operators;

public class LargerThanOperatorTests
{
	[Fact]
	public void LargerThanOperator_BothAreStrongTypedAndOtherIsLarger_IsLarger()
	{
		// Arrange
		var idOne = new IntId(42);
		var idTwo = new IntId(1337);

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.True(isLarger);
	}

	[Fact]
	public void LargerThanOperator_BothAreStrongTypedAndOtherIsNotLarger_IsNotLarger()
	{
		// Arrange
		var idOne = new IntId(1337);
		var idTwo = new IntId(42);

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.False(isLarger);
	}

	[Fact]
	public void LargerThanOperator_BothAreStrongTypedEqual_IsNotLarger()
	{
		// Arrange
		var idOne = new IntId(42);
		var idTwo = new IntId(42);

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.False(isLarger);
	}

	[Fact]
	public void LargerThanOperator_OneIsStrongTypedAndOtherIsLarger_IsLarger()
	{
		// Arrange
		var idOne = new IntId(42);
		var idTwo = 1337;

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.True(isLarger);
	}

	[Fact]
	public void LargerThanOperator_OneIsStrongTypedAndOtherIsNotLarger_IsNotLarger()
	{
		// Arrange
		var idOne = new IntId(1337);
		var idTwo = 42;

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.False(isLarger);
	}

	[Fact]
	public void LargerThanOperator_OneIsStrongTypedEqual_IsNotLarger()
	{
		// Arrange
		var idOne = new IntId(42);
		var idTwo = 42;

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.False(isLarger);
	}


	[Fact]
	public void LargerThanOperator_OtherIsStrongTypedAndOtherIsLarger_IsLarger()
	{
		// Arrange
		var idOne = 42;
		var idTwo = new IntId(1337);

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.True(isLarger);
	}

	[Fact]
	public void LargerThanOperator_OtherIsStrongTypedAndOtherIsNotLarger_IsNotLarger()
	{
		// Arrange
		var idOne = 1337;
		var idTwo = new IntId(42);

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.False(isLarger);
	}

	[Fact]
	public void LargerThanOperator_OtherIsStrongTypedEqual_IsNotLarger()
	{
		// Arrange
		var idOne = 42;
		var idTwo = new IntId(42);

		// Act
		var isLarger = idTwo > idOne;

		// Assert
		Assert.False(isLarger);
	}
}