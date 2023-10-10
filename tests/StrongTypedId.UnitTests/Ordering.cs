using System.Linq;

namespace StrongTypedId.UnitTests;

public class OrderingTests
{
	[Fact]
	public void OrderBy_HasDifferentValues_OrderedCorrectly()
	{
		// Arrange
		var ids = new[] { new IntId(42), new IntId(1337), new IntId(256) };

		// Act
		var ordered = ids.OrderBy(id => id).ToList();

		// Assert
		Assert.Equal(42, ordered[0].Value);
		Assert.Equal(256, ordered[1].Value);
		Assert.Equal(1337, ordered[2].Value);
	}

	[Fact]
	public void OrderByDescending_HasDifferentValues_OrderedCorrectly()
	{
		// Arrange
		var ids = new[] { new IntId(42), new IntId(1337), new IntId(256) };

		// Act
		var ordered = ids.OrderByDescending(id => id).ToList();

		// Assert
		Assert.Equal(1337, ordered[0].Value);
		Assert.Equal(256, ordered[1].Value);
		Assert.Equal(42, ordered[2].Value);
	}
}