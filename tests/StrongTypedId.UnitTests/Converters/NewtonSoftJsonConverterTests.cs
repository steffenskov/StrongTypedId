using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Converters;

public class NewtonSoftJsonConverterTests
{
    [Fact]
    public void Serialize_Guid_SerializedAsGuid()
    {
        // Arrange
        var id = GuidId.New();

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

    [Fact]
    public void Serialize_Int_SerializedAsInt()
    {
        // Arrange
        var id = IntId.Create(42);

        // Act
        var strongIdJson = JsonConvert.SerializeObject(id);
        var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveId);

        // Assert
        Assert.Equal(primitiveIdJson, strongIdJson);
    }

    [Fact]
    public void Deserialize_Int_Deserializes()
    {
        // Arrange
        var intValue = 42;
        var json = JsonConvert.SerializeObject(intValue);

        // Act
        var strongId = JsonConvert.DeserializeObject<IntId>(json);

        // Assert
        Assert.NotNull(strongId);
        Assert.Equal(intValue, strongId!.PrimitiveId);
    }
}