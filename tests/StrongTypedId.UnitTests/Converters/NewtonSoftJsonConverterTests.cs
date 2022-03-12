using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Converters;

public class NewtonSoftJsonConverterTests
{
	[Fact]
	public void Serialize_Guid_SerializedAsGuid()
	{
		// Arrange
		GuidId id = GuidId.New();

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveId);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}


	[Fact]
	public void Deserialize_Guid_Deserializes()
	{
		// Arrange
		var guid = Guid.NewGuid();
		var json = JsonConvert.SerializeObject(guid);

		// Act
		var strongId = JsonConvert.DeserializeObject<GuidId>(json);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(guid, strongId!.PrimitiveId);
	}
}