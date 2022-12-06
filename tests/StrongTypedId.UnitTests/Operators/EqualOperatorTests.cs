namespace StrongTypedId.UnitTests;

public class EqualOperatorTests
{
	[Fact]
	public void EqualOperator_BothAreNull_Equal()
	{
		// Arrange
		GuidId? idOne = null;

		// Act
		var equal = idOne == null;

		// Assert
		Assert.True(equal);
	}

	[Fact]
	public void EqualOperator_OtherIsNull_NotEqual()
	{
		// Arrange
		var idOne = GuidId.New();

		// Act
		var equal = idOne == null;

		// Assert
		Assert.False(equal);
	}

	[Fact]
	public void EqualOperator_BothAreStrongTypedAndEqual_AreEqual()
	{
		// Arrange
		var guid = Guid.NewGuid();
		var idOne = new GuidId(guid);
		var idTwo = new GuidId(guid);

		// Act
		var equal = idOne == idTwo;

		// Assert
		Assert.True(equal);
	}

	[Fact]
	public void EqualOperator_BothAreStrongTypedButNotEqual_NotEqual()
	{
		// Arrange
		var idOne = new GuidId(Guid.NewGuid());
		var idTwo = new GuidId(Guid.NewGuid());

		// Act
		var equal = idOne == idTwo;

		// Assert
		Assert.False(equal);
	}

	[Fact]
	public void EqualOperator_OneIsStrongTypedAndEqual_AreEqual()
	{
		// Arrange
		var idOne = Guid.NewGuid();
		var idTwo = new GuidId(idOne);


		// Act
		var equal = idOne == idTwo;

		// Assert
		Assert.True(equal);
	}

	[Fact]
	public void EqualOperator_OtherIsStrongTypedAndEqual_AreEqual()
	{
		// Arrange
		var idOne = Guid.NewGuid();
		var idTwo = new GuidId(idOne);


		// Act
		var equal = idTwo == idOne;

		// Assert
		Assert.True(equal);
	}

	[Fact]
	public void EqualOperator_OneIsStrongTypedButNotEqual_NotEqual()
	{
		// Arrange
		var idOne = Guid.NewGuid();
		var idTwo = new GuidId(Guid.NewGuid());

		// Act
		var equal = idOne == idTwo;

		// Assert
		Assert.False(equal);
	}

	[Fact]
	public void EqualOperator_OtherIsStrongTypedButNotEqual_NotEqual()
	{
		// Arrange
		var idOne = Guid.NewGuid();
		var idTwo = new GuidId(Guid.NewGuid());

		// Act
		var equal = idTwo == idOne;

		// Assert
		Assert.False(equal);
	}
}