using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Converters;

public class StrongTypedNewtonSoftJsonConverterTests
{
	private readonly JsonSerializerSettings _options;

	public StrongTypedNewtonSoftJsonConverterTests()
	{
		_options = new JsonSerializerSettings
		{
			Converters = [new StrongTypedNewtonSoftJsonConverter()]
		};
	}

	#region Interface

	[Fact]
	public void Serialize_DeclaredAsInterface_TypeInformationDoesNotContainVersion()
	{
		// Arrange

		var aggregate = new FakeAggregate
		{
			Marker = new Marker("Hello world")
		};

		// Act
		var json = JsonConvert.SerializeObject(aggregate, _options);

		// Assert
		Assert.Contains(typeof(Marker).FullName!, json);
		Assert.DoesNotContain("Version", json);
	}

	[Fact]
	public void Serialize_DeclaredAsInterface_ContainsTypeInformation()
	{
		// Arrange
		var aggregate = new FakeAggregate
		{
			Marker = new Marker("Hello world")
		};

		// Act
		var json = JsonConvert.SerializeObject(aggregate, _options);

		// Assert
		Assert.Contains(typeof(Marker).FullName!, json);
	}

	[Fact]
	public void Deserialize_DeclaredAsInterface_CanDeserialize()
	{
		// Arrange
		var aggregate = new FakeAggregate
		{
			Marker = new Marker("Hello world")
		};
		var json = JsonConvert.SerializeObject(aggregate, _options);

		// Act

		var deserialized = JsonConvert.DeserializeObject<FakeAggregate>(json, _options);

		// Assert
		Assert.Equal(aggregate, deserialized);
	}

	#endregion

	#region Guid

	[Fact]
	public void Serialize_Guid_SerializedWithTypeInformation()
	{
		// Arrange
		var id = GuidId.New();

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedGuid_SerializedAsGuid()
	{
		// Arrange
		var id = AttributedGuidId.New();

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawGuid_Deserializes()
	{
		// Arrange
		var guid = Guid.NewGuid();
		var json = JsonConvert.SerializeObject(guid, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<GuidId>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(guid, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_GuidIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<GuidId?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedGuid_Deserializes()
	{
		// Arrange
		var value = GuidId.New();
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<GuidId?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Bool

	[Fact]
	public void Serialize_Bool_SerializedWithTypeInformation()
	{
		// Arrange
		var id = BoolValue.Create(true);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedBool_SerializedAsBool()
	{
		// Arrange
		var id = AttributedBoolValue.Create(true);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawBool_Deserializes()
	{
		// Arrange
		var boolValue = true;
		var json = JsonConvert.SerializeObject(boolValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<BoolValue>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(boolValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_BoolIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<BoolValue?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedBool_Deserializes()
	{
		// Arrange
		var value = BoolValue.Create(true);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<BoolValue?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Byte

	[Fact]
	public void Serialize_Byte_SerializedWithTypeInformation()
	{
		// Arrange
		var id = ByteId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedByte_SerializedAsByte()
	{
		// Arrange
		var id = AttributedByteId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawByte_Deserializes()
	{
		// Arrange
		var byteValue = 42;
		var json = JsonConvert.SerializeObject(byteValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<ByteId>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(byteValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ByteIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<ByteId?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedByte_Deserializes()
	{
		// Arrange
		var value = ByteId.Create(42);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<ByteId?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Char

	[Fact]
	public void Serialize_Char_SerializedWithTypeInformation()
	{
		// Arrange
		var id = CharValue.Create('A');

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedChar_SerializedAsChar()
	{
		// Arrange
		var id = AttributedCharValue.Create('A');

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawChar_Deserializes()
	{
		// Arrange
		var charValue = 'A';
		var json = JsonConvert.SerializeObject(charValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<CharValue>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(charValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_CharIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<CharValue?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedChar_Deserializes()
	{
		// Arrange
		var value = CharValue.Create('A');
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<CharValue?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Date

	[Fact]
	public void Serialize_Date_SerializedWithTypeInformation()
	{
		// Arrange
		var id = DateValue.Create(DateTime.UtcNow);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedDate_SerializedAsDate()
	{
		// Arrange
		var id = AttributedDateValue.Create(DateTime.UtcNow);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawDate_Deserializes()
	{
		// Arrange
		var DateTimeValue = DateTime.UtcNow;
		var json = JsonConvert.SerializeObject(DateTimeValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DateValue>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(DateTimeValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DateIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DateValue?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedDate_Deserializes()
	{
		// Arrange
		var value = DateValue.Create(DateTime.UtcNow);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<DateValue?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Short

	[Fact]
	public void Serialize_Short_SerializedWithTypeInformation()
	{
		// Arrange
		var id = ShortId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedShort_SerializedAsShort()
	{
		// Arrange
		var id = AttributedShortId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawShort_Deserializes()
	{
		// Arrange
		short shortValue = 42;
		var json = JsonConvert.SerializeObject(shortValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<ShortId>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(shortValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ShortIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<ShortId?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedShort_Deserializes()
	{
		// Arrange
		var value = ShortId.Create(42);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<ShortId?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Ushort

	[Fact]
	public void Serialize_Ushort_SerializedWithTypeInformation()
	{
		// Arrange
		var id = UshortId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedUshort_SerializedAsUshort()
	{
		// Arrange
		var id = AttributedUshortId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawUshort_Deserializes()
	{
		// Arrange
		ushort ushortValue = 42;
		var json = JsonConvert.SerializeObject(ushortValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UshortId>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(ushortValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UshortIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UshortId?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedUshort_Deserializes()
	{
		// Arrange
		var value = UshortId.Create(42);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<UshortId?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Int

	[Fact]
	public void Serialize_Int_SerializedWithTypeInformation()
	{
		// Arrange
		var id = IntId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedInt_SerializedAsInt()
	{
		// Arrange
		var id = AttributedIntId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawInt_Deserializes()
	{
		// Arrange
		var intValue = 42;
		var json = JsonConvert.SerializeObject(intValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<IntId>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_IntIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<IntId?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedInt_Deserializes()
	{
		// Arrange
		var value = IntId.Create(42);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<IntId?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Uint

	[Fact]
	public void Serialize_Uint_SerializedWithTypeInformation()
	{
		// Arrange
		var id = UintId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedUint_SerializedAsUint()
	{
		// Arrange
		var id = AttributedUintId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawUint_Deserializes()
	{
		// Arrange
		var uintValue = 42u;
		var json = JsonConvert.SerializeObject(uintValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UintId>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(uintValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UintIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UintId?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedUint_Deserializes()
	{
		// Arrange
		var value = UintId.Create(42);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<UintId?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Long

	[Fact]
	public void Serialize_Long_SerializedWithTypeInformation()
	{
		// Arrange
		var id = LongId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedLong_SerializedAsLong()
	{
		// Arrange
		var id = AttributedLongId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawLong_Deserializes()
	{
		// Arrange
		var longValue = 42L;
		var json = JsonConvert.SerializeObject(longValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<LongId>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(longValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_LongIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<LongId?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedLong_Deserializes()
	{
		// Arrange
		var value = LongId.Create(42);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<LongId?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Ulong

	[Fact]
	public void Serialize_Ulong_SerializedWithTypeInformation()
	{
		// Arrange
		var id = UlongId.Create(42);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedUlong_SerializedAsUlong()
	{
		// Arrange
		var id = AttributedUlongId.Create(42);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawUlong_Deserializes()
	{
		// Arrange
		var ulongValue = 42UL;
		var json = JsonConvert.SerializeObject(ulongValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UlongId>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(ulongValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UlongIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<UlongId?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedUlong_Deserializes()
	{
		// Arrange
		var value = UlongId.Create(42);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<UlongId?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Float

	[Fact]
	public void Serialize_Float_SerializedWithTypeInformation()
	{
		// Arrange
		var id = FloatValue.Create(42.1337f);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedFloat_SerializedAsFloat()
	{
		// Arrange
		var id = AttributedFloatValue.Create(42.1337f);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawFloat_Deserializes()
	{
		// Arrange
		var floatValue = 42.1337f;
		var json = JsonConvert.SerializeObject(floatValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<FloatValue>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(floatValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_FloatIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<FloatValue?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}


	[Fact]
	public void Deserialize_SerializedFloat_Deserializes()
	{
		// Arrange
		var value = FloatValue.Create(42.1337f);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<FloatValue?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Double

	[Fact]
	public void Serialize_Double_SerializedWithTypeInformation()
	{
		// Arrange
		var id = DoubleValue.Create(42.1337);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedDouble_SerializedAsDouble()
	{
		// Arrange
		var id = AttributedDoubleValue.Create(42.1337);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawDouble_Deserializes()
	{
		// Arrange
		var doubleValue = 42.1337;
		var json = JsonConvert.SerializeObject(doubleValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DoubleValue>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(doubleValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DoubleIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DoubleValue?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}


	[Fact]
	public void Deserialize_SerializedDouble_Deserializes()
	{
		// Arrange
		var value = DoubleValue.Create(42.1337);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<DoubleValue?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region Decimal

	[Fact]
	public void Serialize_Decimal_SerializedWithTypeInformation()
	{
		// Arrange
		var id = DecimalValue.Create(42.1337m);

		// Act
		var json = JsonConvert.SerializeObject(id, _options);

		// Assert
		Assert.Contains(id.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedDecimal_SerializedAsDecimal()
	{
		// Arrange
		var id = AttributedDecimalValue.Create(42.1337m);

		// Act
		var strongIdJson = JsonConvert.SerializeObject(id, _options);
		var primitiveIdJson = JsonConvert.SerializeObject(id.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_RawDecimal_Deserializes()
	{
		// Arrange
		var decimalValue = 42.1337m;
		var json = JsonConvert.SerializeObject(decimalValue, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DecimalValue>(json, _options);

		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(decimalValue, strongId!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DecimalIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<DecimalValue?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_SerializedDecimal_Deserializes()
	{
		// Arrange
		var value = DecimalValue.Create(42.1337m);
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<DecimalValue?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
	}

	#endregion

	#region String

	[Fact]
	public void Serialize_String_SerializedWithTypeInformation()
	{
		// Arrange

		var value = EmailAddress.Create("Hello");

		// Act
		var json = JsonConvert.SerializeObject(value, _options);

		// Assert
		Assert.Contains(value.GetType().FullName!, json);
	}

	[Fact]
	public void Serialize_AttributedString_SerializedAsString()
	{
		// Arrange
		var str = AttributedEmailAddress.Create("Hello");

		// Act
		var strongJson = JsonConvert.SerializeObject(str, _options);
		var primitiveJson = JsonConvert.SerializeObject(str.PrimitiveValue);

		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_StringIsNull_Deserializes()
	{
		// Arrange
		var json = JsonConvert.SerializeObject(null, _options);

		// Act
		var strongId = JsonConvert.DeserializeObject<EmailAddress?>(json, _options);

		// Assert
		Assert.Null(strongId);
	}

	[Fact]
	public void Deserialize_RawString_Deserializes()
	{
		// Arrange
		var stringValue = "Hello world";

		var json = JsonConvert.SerializeObject(stringValue, _options);

		// Act
		var strongValue = JsonConvert.DeserializeObject<EmailAddress>(json, _options);

		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(stringValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_SerializedString_Deserializes()
	{
		// Arrange
		var value = EmailAddress.Create("Hello world");
		var json = JsonConvert.SerializeObject(value, _options);

		// Act
		var deserializedValue = JsonConvert.DeserializeObject<EmailAddress?>(json, _options);

		// Assert
		Assert.Equal(value, deserializedValue);
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

public partial class Marker : StrongTypedValue<Marker, string>, IMarker
{
	public Marker(string primitiveValue) : base(primitiveValue)
	{
	}
}