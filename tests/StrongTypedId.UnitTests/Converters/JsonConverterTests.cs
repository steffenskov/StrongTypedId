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
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);

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
		Assert.Equal(guid, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_GuidIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((GuidId?)null);

		// Act
		var strongId = JsonSerializer.Deserialize<GuidId?>(json);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Serialize_Int_SerializedAsInt()
	{
		// Arrange
		var id = IntId.Create(42);

		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);

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
		var strongId = JsonSerializer.Deserialize<IntId>(json);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_IntIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((IntId?)null);

		// Act
		var strongId = JsonSerializer.Deserialize<IntId?>(json);

		// Assert
		Assert.Null(strongId);
	}


	[Fact]
	public void Serialize_String_SerializedAsString()
	{
		// Arrange
		var str = EmailAddress.Create("Hello");

		// Act
		var strongJson = JsonSerializer.Serialize(str);
		var primitiveJson = JsonSerializer.Serialize(str.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_StringIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((EmailAddress?)null);

		// Act
		var strongId = JsonSerializer.Deserialize<EmailAddress?>(json);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_String_Deserializes()
	{
		// Arrange
		var stringValue = "Hello world";
		var json = JsonSerializer.Serialize(stringValue);

		// Act
		var strongValue = JsonSerializer.Deserialize<EmailAddress>(json);

		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(stringValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Serialize_UsedAsDictionaryKey_Serializes()
	{
		// Arrange
		var aggregate = new BasicAggregateWithDictionary(new Dictionary<GuidId, string[]>
		{
			{ GuidId.New(), new[] { "Hello", "world" } }
		});

		// Act
		var json = JsonSerializer.Serialize(aggregate);

		// Assert
		Assert.NotNull(json);
	}

	[Fact]
	public void Deserialize_UsedAsDictionaryKey_Deserializes()
	{
		// Arrange
		var id = GuidId.New();
		var aggregate = new BasicAggregateWithDictionary(new Dictionary<GuidId, string[]>
		{
			{ id, new[] { "Hello", "world" } }
		});
		var json = JsonSerializer.Serialize(aggregate);

		// Act
		var deserialized = JsonSerializer.Deserialize<BasicAggregateWithDictionary>(json);

		// Assert
		Assert.NotNull(deserialized);
		Assert.Contains(id, deserialized.Dictionary.Keys);
		Assert.Equal(2, deserialized.Dictionary[id].Length);
	}
}

file class BasicAggregateWithDictionary
{
	public BasicAggregateWithDictionary()
	{
	}

	public BasicAggregateWithDictionary(Dictionary<GuidId, string[]> value)
	{
		Dictionary = value;
	}

	public Dictionary<GuidId, string[]> Dictionary { get; set; } = default!;
}