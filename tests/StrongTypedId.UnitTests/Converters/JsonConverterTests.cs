using System.Text.Json;

namespace StrongTypedId.UnitTests.Converters;

public class JsonConverterTests
{
    [Fact]
    public void Serialize_Guid_SerializedAsGuid()
    {
        // Arrange
        var id = GuidId.New();

        // Act
        var strongIdJson = JsonSerializer.Serialize(id, new JsonSerializerOptions
        {
            Converters =
            {
                new SystemTextJsonConverter<IntId, int>()
            }
        });
        var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveId);

        // Assert
        Assert.Equal(primitiveIdJson, strongIdJson);
    }

    [Fact]
    public void Deserialize_Guid_Deserializes()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var json = JsonSerializer.Serialize(guid, new JsonSerializerOptions
        {
            Converters =
            {
                new SystemTextJsonConverter<IntId, int>()
            }
        });

        // Act
        var strongId = JsonSerializer.Deserialize<GuidId>(json, new JsonSerializerOptions
        {
            Converters =
            {
                new SystemTextJsonConverter<IntId, int>()
            }
        });

        // Assert
        Assert.NotNull(strongId);
        Assert.Equal(guid, strongId!.PrimitiveId);
    }

    [Fact]
    public void Serialize_Int_SerializedAsInt()
    {
        // Arrange
        var id = IntId.New();

        // Act
        var strongIdJson = JsonSerializer.Serialize(id, new JsonSerializerOptions
        {
            Converters =
            {
                new SystemTextJsonConverter<IntId, int>()
            }
        });
        var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveId);

        // Assert
        Assert.Equal(primitiveIdJson, strongIdJson);
    }

    [Fact]
    public void Deserialize_Int_Deserializes()
    {
        // Arrange
        var intValue = 42;
        var json = JsonSerializer.Serialize(intValue);

        // Act
        var strongId = JsonSerializer.Deserialize<IntId>(json, new JsonSerializerOptions
        {
            Converters =
            {
                new SystemTextJsonConverter<IntId, int>()
            }
        });

        // Assert
        Assert.NotNull(strongId);
        Assert.Equal(intValue, strongId!.PrimitiveId);
    }
}