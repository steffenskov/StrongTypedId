namespace StrongTypedId.UnitTests;

public class UnequalOperatorTests
{
	[Fact]
	public void EqualOperator_BothAreNull_Equal()
	{
		// Arrange
		GuidId? idOne = null;

		// Act
		var unequal = idOne != null;

		// Assert
		Assert.False(unequal);
	}

	[Fact]
	public void UnequalOperator_OtherIsNull_AreUnequal()
	{
		// Arrange
		var idOne = GuidId.New();

		// Act
		var unequal = idOne != null;

		// Assert
		Assert.True(unequal);
	}

	[Fact]
	public void UnequalOperator_BothAreStrongTypedAndUnequal_AreUnequal()
	{
		// Arrange
		var idOne = new GuidId(Guid.NewGuid());
		var idTwo = new GuidId(Guid.NewGuid());

		// Act
		var unequal = idOne != idTwo;

		// Assert
		Assert.True(unequal);
	}

	[Fact]
	public void UnequalOperator_BothAreStrongTypedButNotUnequal_NotUnequal()
	{
		// Arrange
		var guid = Guid.NewGuid();
		var idOne = new GuidId(guid);
		var idTwo = new GuidId(guid);

		// Act
		var unequal = idOne != idTwo;

		// Assert
		Assert.False(unequal);
	}

	[Fact]
	public void UnequalOperator_OneIsStrongTypedAndUnequal_AreUnequal()
	{
		// Arrange
		var idOne = Guid.NewGuid();
		var idTwo = new GuidId(Guid.NewGuid());

		// Act
		var unequal = idOne != idTwo;

		// Assert
		Assert.True(unequal);
	}

	[Fact]
	public void UnequalOperator_OtherIsStrongTypedAndUnequal_AreUnequal()
	{
		// Arrange
		var idOne = Guid.NewGuid();
		var idTwo = new GuidId(Guid.NewGuid());

		// Act
		var unequal = idTwo != idOne;

		// Assert
		Assert.True(unequal);
	}

	[Fact]
	public void UnequalOperator_OneIsStrongTypedButNotUnequal_NotUnequal()
	{
		// Arrange
		var idOne = Guid.NewGuid();
		var idTwo = new GuidId(idOne);

		// Act
		var unequal = idOne != idTwo;

		// Assert
		Assert.False(unequal);
	}

	[Fact]
	public void UnequalOperator_OtherIsStrongTypedButNotUnequal_NotUnequal()
	{
		// Arrange
		var idOne = Guid.NewGuid();
		var idTwo = new GuidId(idOne);

		// Act
		var unequal = idTwo != idOne;

		// Assert
		Assert.False(unequal);
	}
}