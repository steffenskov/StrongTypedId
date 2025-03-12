namespace StrongTypedId.UnitTests.Parsable;

public class ParsableTests
{
	[Fact]
	public void Parse_IsParsable_ReturnsValue()
	{
		// Arrange
		var guid = Guid.NewGuid();
		var text = guid.ToString();

		// Act
		var strongId = AttributedGuidId.Parse(text);

		// Assert
		Assert.Equal(guid, strongId.PrimitiveValue);
	}

	[Fact]
	public void Parse_NotParsable_Throws()
	{
		// Arrange
		var text = "Hello world";

		// Act && Assert
		Assert.Throws<FormatException>(() => AttributedGuidId.Parse(text));
	}

	[Fact]
	public void TryParse_IsParsable_ReturnsTrue()
	{
		// Arrange
		var guid = Guid.NewGuid();
		var text = guid.ToString();

		// Act
		var parsed = AttributedGuidId.TryParse(text, out var strongId);

		// Assert
		Assert.True(parsed);
		Assert.NotNull(strongId);
		Assert.Equal(guid, strongId.PrimitiveValue);
	}

	[Fact]
	public void TryParse_NotParsable_ReturnsFalse()
	{
		// Arrange
		var text = "Hello world";

		// Act
		var parsed = AttributedGuidId.TryParse(text, out var strongId);

		// Assert
		Assert.False(parsed);
		Assert.Null(strongId);
	}
}