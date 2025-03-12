using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Converters;

public class StrongTypedNewtonSoftJsonConverterTests
{
	#region Interface

	[Fact]
	public void Serialize_DeclaredAsInterfaceWithoutTypeHandling_DoesNotContainTypeInformation()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.None,
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var aggregate = new FakeAggregate
		{
			Marker = new Marker("Hello world")
		};

		// Act
		var json = JsonConvert.SerializeObject(aggregate, options);

		// Assert
		Assert.DoesNotContain(typeof(Marker).FullName!, json);
	}

	[Fact]
	public void Serialize_DeclaredAsInterface_TypeInformationDoesNotContainVersion()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto,
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var aggregate = new FakeAggregate
		{
			Marker = new Marker("Hello world")
		};

		// Act
		var json = JsonConvert.SerializeObject(aggregate, options);

		// Assert
		Assert.Contains(typeof(Marker).FullName!, json);
		Assert.DoesNotContain("Version", json);
	}

	[Fact]
	public void Serialize_DeclaredAsInterface_ContainsTypeInformation()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto,
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var aggregate = new FakeAggregate
		{
			Marker = new Marker("Hello world")
		};

		// Act
		var json = JsonConvert.SerializeObject(aggregate, options);

		// Assert
		Assert.Contains(typeof(Marker).FullName!, json);
	}

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Deserialize_DeclaredAsInterfaceWithTypeHandling_CanDeserialize(TypeNameHandling typeNameHandling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			TypeNameHandling = typeNameHandling,
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var aggregate = new FakeAggregate
		{
			Marker = new Marker("Hello world")
		};
		var json = JsonConvert.SerializeObject(aggregate, options);

		// Act

		var deserialized = JsonConvert.DeserializeObject<FakeAggregate>(json, options);

		// Assert
		Assert.Equal(aggregate, deserialized);
	}

	[Fact]
	public void Deserialize_DeclaredAsInterfaceWithoutTypeHandling_CannotDeserialize()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.None,
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var aggregate = new FakeAggregate
		{
			Marker = new Marker("Hello world")
		};
		var json = JsonConvert.SerializeObject(aggregate, options);

		// Act && Assert
		var ex = Assert.Throws<InvalidOperationException>(() => JsonConvert.DeserializeObject<FakeAggregate>(json, options));
		Assert.Contains("Cannot deserialize interface or abstract type when JSON doesn't contain $type", ex.Message);
	}

	#endregion

	#region Guid

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_GuidWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = GuidId.New();

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_GuidWithoutTypeHandling_SerializedAsGuid()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = GuidId.New();

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawGuid_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var guid = Guid.NewGuid();
		var json = JsonConvert.SerializeObject(guid, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<GuidId>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(guid, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_GuidIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<GuidId?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Byte

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_ByteWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = ByteId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_ByteWithoutTypeHandling_SerializedAsByte()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = ByteId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawByte_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var byteValue = 42;
		var json = JsonConvert.SerializeObject(byteValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<ByteId>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(byteValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ByteIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<ByteId?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Char

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_CharWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = CharValue.Create('A');

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_CharWithoutTypeHandling_SerializedAsChar()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = CharValue.Create('A');

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawChar_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var charValue = 'A';
		var json = JsonConvert.SerializeObject(charValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<CharValue>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(charValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_CharIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<CharValue?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Date

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_DateWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = DateValue.Create(DateTime.UtcNow);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_DateWithoutTypeHandling_SerializedAsDate()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = DateValue.Create(DateTime.UtcNow);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawDate_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var DateTimeValue = DateTime.UtcNow;
		var json = JsonConvert.SerializeObject(DateTimeValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DateValue>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(DateTimeValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DateIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DateValue?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Short

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_ShortWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = ShortId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_ShortWithoutTypeHandling_SerializedAsShort()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = ShortId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawShort_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		short shortValue = 42;
		var json = JsonConvert.SerializeObject(shortValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<ShortId>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(shortValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ShortIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<ShortId?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Ushort

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_UshortWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = UshortId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_UshortWithoutTypeHandling_SerializedAsUshort()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = UshortId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawUshort_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		ushort ushortValue = 42;
		var json = JsonConvert.SerializeObject(ushortValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UshortId>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(ushortValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UshortIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UshortId?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Int

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_IntWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = IntId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_IntWithoutTypeHandling_SerializedAsInt()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = IntId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawInt_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var intValue = 42;
		var json = JsonConvert.SerializeObject(intValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<IntId>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_IntIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<IntId?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Uint

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_UintWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = UintId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_UintWithoutTypeHandling_SerializedAsUint()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = UintId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawUint_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var uintValue = 42u;
		var json = JsonConvert.SerializeObject(uintValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UintId>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(uintValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UintIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UintId?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Long

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_LongWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = LongId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_LongWithoutTypeHandling_SerializedAsLong()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = LongId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawLong_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var longValue = 42L;
		var json = JsonConvert.SerializeObject(longValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<LongId>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(longValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_LongIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<LongId?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Ulong

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_UlongWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = UlongId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_UlongWithoutTypeHandling_SerializedAsUlong()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = UlongId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawUlong_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var ulongValue = 42UL;
		var json = JsonConvert.SerializeObject(ulongValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UlongId>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(ulongValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UlongIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UlongId?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Float

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_FloatWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = FloatValue.Create(42.1337f);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_FloatWithoutTypeHandling_SerializedAsFloat()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = FloatValue.Create(42.1337f);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawFloat_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var floatValue = 42.1337f;
		var json = JsonConvert.SerializeObject(floatValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<FloatValue>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(floatValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_FloatIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<FloatValue?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Double

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_DoubleWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = DoubleValue.Create(42.1337);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_DoubleWithoutTypeHandling_SerializedAsDouble()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = DoubleValue.Create(42.1337);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawDouble_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var doubleValue = 42.1337;
		var json = JsonConvert.SerializeObject(doubleValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DoubleValue>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(doubleValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DoubleIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DoubleValue?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Decimal

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_DecimalWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var id = DecimalValue.Create(42.1337m);

		// Act
		var json = JsonConvert.SerializeObject(id, options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_DecimalWithoutTypeHandling_SerializedAsDecimal()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var id = DecimalValue.Create(42.1337m);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawDecimal_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var decimalValue = 42.1337m;
		var json = JsonConvert.SerializeObject(decimalValue, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DecimalValue>(json, options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(decimalValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DecimalIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DecimalValue?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region String

	[Theory]
	[InlineData(TypeNameHandling.Auto)]
	[InlineData(TypeNameHandling.Objects)]
	[InlineData(TypeNameHandling.All)]
	public void Serialize_StringWithTypeHandling_SerializedWithTypeInformation(TypeNameHandling handling)
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()],
			TypeNameHandling = handling
		};
		var value = EmailAddress.Create("Hello");

		// Act
		var json = JsonConvert.SerializeObject(value, options);

		// Assert
		Assert.Contains(value.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_StringWithoutTypeHandling_SerializedAsString()
	{
		// Arrange
		var str = EmailAddress.Create("Hello");
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};

		// Act
		var strongJson = JsonConvert.SerializeObject(str, options);
		var primitiveJson = JsonConvert.SerializeObject(str.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_StringIsNull_Deserializes()
	{
		// Arrange
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
		var json = JsonConvert.SerializeObject(null, options);

		// Act
		var strongId = JsonConvert.DeserializeObject<EmailAddress?>(json, options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_RawString_Deserializes()
	{
		// Arrange
		var stringValue = "Hello world";
		var options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};

		var json = JsonConvert.SerializeObject(stringValue, options);

		// Act
		var strongValue = JsonConvert.DeserializeObject<EmailAddress>(json, options);

		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(stringValue, strongValue!.PrimitiveValue);
	}

	#endregion
}

file record FakeAggregate
{
	public IMarker Marker { get; init; } = default!;
}

file interface IMarker : IStrongTypedValue<string>
{
}

[TypeConverter(typeof(StrongTypedValueTypeConverter<Marker, string>))]
file class Marker : StrongTypedValue<Marker, string>, IMarker
{
	public Marker(string primitiveValue) : base(primitiveValue)
	{
	}
}