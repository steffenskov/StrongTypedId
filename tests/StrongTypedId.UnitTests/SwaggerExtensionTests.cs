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
		options.MapStrongType<AttributedGuidId, Guid>();
		options.MapStrongType<AttributedIntId, int>();

		// Assert
		Assert.True(true); // Just testing no exceptions were thrown
	}

	[Fact]
	public void MapStrongType_NonGenericOverload_DoesNotThrow()
	{
		// Arrange
		var options = new SwaggerGenOptions();

		// Act
		options.MapStrongType(typeof(AttributedGuidId), typeof(Guid));
		options.MapStrongType(typeof(AttributedIntId), typeof(int));

		// Assert
		Assert.True(true); // Just testing no exceptions were thrown
	}
}