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
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

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
		Assert.Equal(guid, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_GuidIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null);

		// Act
		var strongId = JsonConvert.DeserializeObject<GuidId?>(json);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Serialize_Int_SerializedAsInt()
	{
		// Arrange
		var id = IntId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

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
		Assert.Equal(intValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_IntIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null);

		// Act
		var strongId = JsonConvert.DeserializeObject<IntId?>(json);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Serialize_String_SerializedAsString()
	{
		// Arrange
		var str = EmailAddress.Create("Hello");

		// Act
		var strongJson = JsonConvert.SerializeObject(str);
		var primitiveJson = JsonConvert.SerializeObject(str.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_StringIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null);

		// Act
		var strongId = JsonConvert.DeserializeObject<EmailAddress?>(json);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_String_Deserializes()
	{
		// Arrange
		var stringValue = "Hello world";
		var json = JsonConvert.SerializeObject(stringValue);

		// Act
		var strongValue = JsonConvert.DeserializeObject<EmailAddress>(json);

		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(stringValue, strongValue!.PrimitiveValue);
	}
}