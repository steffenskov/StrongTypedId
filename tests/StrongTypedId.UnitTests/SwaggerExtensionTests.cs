using Swashbuckle.AspNetCore.SwaggerGen;

namespace StrongTypedId.UnitTests;

public class SwaggerExtensionTests
{
    [Fact]
    public void MapStrongType_GenericOverload_DoesNotThrow()
    {
        // Arrange
        var options = new SwaggerGenOptions();
        
        // Act
        options.MapStrongType<GuidId, Guid>();
        options.MapStrongType<IntId, int>();
        
        // Assert
        Assert.True(true); // Just testing no exceptions were thrown
    }
    
    [Fact]
    public void MapStrongType_NonGenericOverload_DoesNotThrow()
    {
        // Arrange
        var options = new SwaggerGenOptions();
        
        // Act
        options.MapStrongType(typeof(GuidId), typeof(Guid));
        options.MapStrongType(typeof(IntId), typeof(int));
        
        // Assert
        Assert.True(true); // Just testing no exceptions were thrown
    }
}