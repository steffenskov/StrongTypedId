using System.Text.Json;

namespace StrongTypedId.UnitTests.Converters;

public class JsonConverterTests
{
	[Fact]
	public void Serialize_Guid_SerializedAsGuid()
	{
		// Arrange
		GuidId id = GuidId.New();

		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveId);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}


	[Fact]
	public void Deserialize_Guid_Deserializes()
	{
		// Arrange
		var guid = Guid.NewGuid();
		var json = JsonSerializer.Serialize(guid);

		// Act
		var strongId = JsonSerializer.Deserialize<GuidId>(json);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(guid, strongId!.PrimitiveId);
	}
}