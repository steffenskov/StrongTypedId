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
        var strongId = GuidId.Parse(text);
        
        // Assert
        Assert.Equal(guid, strongId.PrimitiveValue);
    }
    
    [Fact]
    public void Parse_NotParsable_Throws()
    {
        // Arrange
        var text = "Hello world";
        
        // Act && Assert
        Assert.Throws<FormatException>(() => GuidId.Parse(text));
    }
    
    [Fact]
    public void TryParse_IsParsable_ReturnsTrue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var text = guid.ToString();
        
        // Act
        var parsed = GuidId.TryParse(text, out var strongId);
        
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
        var parsed = GuidId.TryParse(text, out var strongId);

        // Assert
        Assert.False(parsed);
        Assert.Null(strongId);
    }
}