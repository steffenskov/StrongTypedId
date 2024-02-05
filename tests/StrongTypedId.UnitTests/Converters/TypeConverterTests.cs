namespace StrongTypedId.UnitTests.Converters;

public class TypeConverterTests
{
	[Fact]
	public void Serialize_Guid_SerializedAsGuid()
	{
		// Arrange
		var id = GuidId.New();
		var converter = TypeDescriptor.GetConverter(typeof(GuidId));

		// Act
		var strongIdJson = converter.ConvertToString(id);
		var primitiveIdJson = converter.ConvertToString(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_Guid_Deserializes()
	{
		// Arrange
		var converter = TypeDescriptor.GetConverter(typeof(GuidId));
		var guid = Guid.NewGuid();
		var json = converter.ConvertToString(guid);

		// Act
		var strongId = converter.ConvertFrom(json!) as GuidId;

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(guid, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_GuidIsNull_Deserializes()
	{
		// Arrange
		var converter = TypeDescriptor.GetConverter(typeof(GuidId));
		var json = converter.ConvertToString(null);

		// Act
		var strongId = converter.ConvertFrom(json!) as GuidId;

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Serialize_Int_SerializedAsInt()
	{
		// Arrange
		var converter = TypeDescriptor.GetConverter(typeof(IntId));
		var id = IntId.Create(42);

		// Act
		var strongIdJson = converter.ConvertToString(id);
		var primitiveIdJson = converter.ConvertToString(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_Int_Deserializes()
	{
		// Arrange
		var converter = TypeDescriptor.GetConverter(typeof(IntId));
		var intValue = 42;
		var json = converter.ConvertToString(intValue);

		// Act
		var strongId = converter.ConvertFrom(json!) as IntId;

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_IntIsNull_Deserializes()
	{
		// Arrange
		var converter = TypeDescriptor.GetConverter(typeof(IntId));
		var json = converter.ConvertToString(null);

		// Act
		var strongId = converter.ConvertFrom(json!) as IntId;

		// Assert
		Assert.Null(strongId);
	}


	[Fact]
	public void Serialize_String_SerializedAsString()
	{
		// Arrange
		var converter = TypeDescriptor.GetConverter(typeof(EmailAddress));
		var str = EmailAddress.Create("Hello");

		// Act
		var strongJson = converter.ConvertToString(str);
		var primitiveJson = converter.ConvertToString(str.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_StringIsNull_Deserializes()
	{
		// Arrange
		var converter = TypeDescriptor.GetConverter(typeof(EmailAddress));
		var json = converter.ConvertToString(null);

		// Act
		var strongId = converter.ConvertFrom(json!) as EmailAddress;

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_String_Deserializes()
	{
		// Arrange
		var converter = TypeDescriptor.GetConverter(typeof(EmailAddress));
		var stringValue = "Hello world";
		var json = converter.ConvertToString(stringValue);

		// Act
		var strongValue = converter.ConvertFrom(json!) as EmailAddress;

		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(stringValue, strongValue!.PrimitiveValue);
	}
}