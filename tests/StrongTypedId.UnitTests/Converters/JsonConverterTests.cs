using System.Globalization;
using System.Text.Json;

namespace StrongTypedId.UnitTests.Converters;

public class JsonConverterTests
{
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

	#region Bool

	[Fact]
	public void Serialize_Bool_SerializedAsBool()
	{
		// Arrange
		var id = BoolValue.Create(true);
		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_Bool_Deserializes()
	{
		// Arrange
		var rawValue = true;
		var json = JsonSerializer.Serialize(rawValue);
		// Act
		var strongId = JsonSerializer.Deserialize<BoolValue>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(rawValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_BoolAsString_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize(true.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<BoolValue>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.True(strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_BoolIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((BoolValue?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<BoolValue?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Byte

	[Fact]
	public void Serialize_Byte_SerializedAsByte()
	{
		// Arrange
		var id = ByteId.Create(42);
		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_Byte_Deserializes()
	{
		// Arrange
		var intValue = 42;
		var json = JsonSerializer.Serialize(intValue);
		// Act
		var strongId = JsonSerializer.Deserialize<ByteId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ByteAsString_Deserializes()
	{
		// Arrange
		var intValue = 42;
		var json = JsonSerializer.Serialize(intValue.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<ByteId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ByteIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((ByteId?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<ByteId?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region SByte

	[Fact]
	public void Serialize_SByte_SerializedAsSByte()
	{
		// Arrange
		var id = SByteId.Create(42);
		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_SByte_Deserializes()
	{
		// Arrange
		var intValue = 42;
		var json = JsonSerializer.Serialize(intValue);
		// Act
		var strongId = JsonSerializer.Deserialize<SByteId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_SByteAsString_Deserializes()
	{
		// Arrange
		var intValue = 42;
		var json = JsonSerializer.Serialize(intValue.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<SByteId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_SByteIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((SByteId?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<SByteId?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Char

	[Fact]
	public void Serialize_Char_SerializedAsChar()
	{
		// Arrange
		var value = CharValue.Create('A');
		// Act
		var strongJson = JsonSerializer.Serialize(value);
		var primitiveJson = JsonSerializer.Serialize(value.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_Char_Deserializes()
	{
		// Arrange
		var charValue = 'X';
		var json = JsonSerializer.Serialize(charValue);
		// Act
		var strongValue = JsonSerializer.Deserialize<CharValue>(json);
		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(charValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_CharIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((CharValue?)null);
		// Act
		var strongValue = JsonSerializer.Deserialize<CharValue?>(json);
		// Assert
		Assert.Null(strongValue);
	}

	#endregion

	#region Date

	[Fact]
	public void Serialize_Date_SerializedAsDate()
	{
		// Arrange
		var value = DateValue.Create(DateTime.Now);
		// Act
		var strongIdJson = JsonSerializer.Serialize(value);
		var primitiveIdJson = JsonSerializer.Serialize(value.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_Date_Deserializes()
	{
		// Arrange
		var dateValue = DateTime.Now;
		var json = JsonSerializer.Serialize(dateValue);
		// Act
		var strongId = JsonSerializer.Deserialize<DateValue>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(dateValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DateAsString_Deserializes()
	{
		// Arrange
		var dateValue = DateTime.Now;
		var json = JsonSerializer.Serialize(dateValue.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture));

		// Act
		var strongId = JsonSerializer.Deserialize<DateValue>(json);

		// Assert
		Assert.NotNull(strongId);

		// Compare with second precision since that's what the format preserves
		Assert.Equal(dateValue.AddTicks(-(dateValue.Ticks % TimeSpan.TicksPerSecond)), strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DateIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((DateValue?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<DateValue?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Decimal

	[Fact]
	public void Serialize_Decimal_SerializedAsDecimal()
	{
		// Arrange
		var value = DecimalValue.Create(123.45m);
		// Act
		var strongJson = JsonSerializer.Serialize(value);
		var primitiveJson = JsonSerializer.Serialize(value.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_Decimal_Deserializes()
	{
		// Arrange
		var decimalValue = 67.89m;
		var json = JsonSerializer.Serialize(decimalValue);
		// Act
		var strongValue = JsonSerializer.Deserialize<DecimalValue>(json);
		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(decimalValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DecimalAsString_Deserializes()
	{
		// Arrange
		var decimalValue = 67.89m;
		var json = JsonSerializer.Serialize(decimalValue.ToString(CultureInfo.InvariantCulture));
		// Act
		var strongValue = JsonSerializer.Deserialize<DecimalValue>(json);
		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(decimalValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DecimalIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((DecimalValue?)null);
		// Act
		var strongValue = JsonSerializer.Deserialize<DecimalValue?>(json);
		// Assert
		Assert.Null(strongValue);
	}

	#endregion

	#region Double

	[Fact]
	public void Serialize_Double_SerializedAsDouble()
	{
		// Arrange
		var value = DoubleValue.Create(123.45);
		// Act
		var strongJson = JsonSerializer.Serialize(value);
		var primitiveJson = JsonSerializer.Serialize(value.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_Double_Deserializes()
	{
		// Arrange
		var doubleValue = 67.89;
		var json = JsonSerializer.Serialize(doubleValue);
		// Act
		var strongValue = JsonSerializer.Deserialize<DoubleValue>(json);
		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(doubleValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DoubleAsString_Deserializes()
	{
		// Arrange
		var doubleValue = 67.89;
		var json = JsonSerializer.Serialize(doubleValue.ToString(CultureInfo.InvariantCulture));
		// Act
		var strongValue = JsonSerializer.Deserialize<DoubleValue>(json);
		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(doubleValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_DoubleIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((DoubleValue?)null);
		// Act
		var strongValue = JsonSerializer.Deserialize<DoubleValue?>(json);
		// Assert
		Assert.Null(strongValue);
	}

	#endregion

	#region EmailAddress

	[Fact]
	public void Serialize_EmailAddress_SerializedAsString()
	{
		// Arrange
		var str = EmailAddress.Create("test@example.com");
		// Act
		var strongJson = JsonSerializer.Serialize(str);
		var primitiveJson = JsonSerializer.Serialize(str.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_EmailAddress_Deserializes()
	{
		// Arrange
		var stringValue = "user@domain.com";
		var json = JsonSerializer.Serialize(stringValue);
		// Act
		var strongValue = JsonSerializer.Deserialize<EmailAddress>(json);
		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(stringValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_EmailAddressIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((EmailAddress?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<EmailAddress?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Float

	[Fact]
	public void Serialize_Float_SerializedAsFloat()
	{
		// Arrange
		var value = FloatValue.Create(123.45f);
		// Act
		var strongJson = JsonSerializer.Serialize(value);
		var primitiveJson = JsonSerializer.Serialize(value.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveJson, strongJson);
	}

	[Fact]
	public void Deserialize_Float_Deserializes()
	{
		// Arrange
		var floatValue = 67.89f;
		var json = JsonSerializer.Serialize(floatValue);
		// Act
		var strongValue = JsonSerializer.Deserialize<FloatValue>(json);
		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(floatValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_FloatAsString_Deserializes()
	{
		// Arrange
		var floatValue = 67.89f;
		var json = JsonSerializer.Serialize(floatValue.ToString(CultureInfo.InvariantCulture));
		// Act
		var strongValue = JsonSerializer.Deserialize<FloatValue>(json);
		// Assert
		Assert.NotNull(strongValue);
		Assert.Equal(floatValue, strongValue!.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_FloatIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((FloatValue?)null);
		// Act
		var strongValue = JsonSerializer.Deserialize<FloatValue?>(json);
		// Assert
		Assert.Null(strongValue);
	}

	#endregion

	#region Guid

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
		Assert.Equal(guid, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_GuidAsString_Deserializes()
	{
		// Arrange
		var guid = Guid.NewGuid();
		var json = JsonSerializer.Serialize(guid.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<GuidId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(guid, strongId.PrimitiveValue);
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

	#endregion

	#region Int

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
		Assert.Equal(intValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_IntAsString_Deserializes()
	{
		// Arrange
		var intValue = 42;
		var json = JsonSerializer.Serialize(intValue.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<IntId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(intValue, strongId.PrimitiveValue);
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

	#endregion

	#region Long

	[Fact]
	public void Serialize_Long_SerializedAsLong()
	{
		// Arrange
		var id = LongId.Create(123456789L);
		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_Long_Deserializes()
	{
		// Arrange
		var longValue = 987654321L;
		var json = JsonSerializer.Serialize(longValue);
		// Act
		var strongId = JsonSerializer.Deserialize<LongId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(longValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_LongAsString_Deserializes()
	{
		// Arrange
		var longValue = 987654321L;
		var json = JsonSerializer.Serialize(longValue.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<LongId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(longValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_LongIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((LongId?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<LongId?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region Short

	[Fact]
	public void Serialize_Short_SerializedAsShort()
	{
		// Arrange
		var id = ShortId.Create(123);
		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_Short_Deserializes()
	{
		// Arrange
		var shortValue = (short)456;
		var json = JsonSerializer.Serialize(shortValue);
		// Act
		var strongId = JsonSerializer.Deserialize<ShortId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(shortValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ShortAsString_Deserializes()
	{
		// Arrange
		var shortValue = (short)456;
		var json = JsonSerializer.Serialize(shortValue.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<ShortId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(shortValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ShortIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((ShortId?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<ShortId?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region UInt

	[Fact]
	public void Serialize_UInt_SerializedAsUInt()
	{
		// Arrange
		var id = UintId.Create(42u);
		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_UInt_Deserializes()
	{
		// Arrange
		var uintValue = 123u;
		var json = JsonSerializer.Serialize(uintValue);
		// Act
		var strongId = JsonSerializer.Deserialize<UintId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(uintValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UIntAsString_Deserializes()
	{
		// Arrange
		var uintValue = 123u;
		var json = JsonSerializer.Serialize(uintValue.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<UintId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(uintValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UIntIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((UintId?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<UintId?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region ULong

	[Fact]
	public void Serialize_ULong_SerializedAsULong()
	{
		// Arrange
		var id = UlongId.Create(123456789UL);
		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_ULong_Deserializes()
	{
		// Arrange
		var ulongValue = 987654321UL;
		var json = JsonSerializer.Serialize(ulongValue);
		// Act
		var strongId = JsonSerializer.Deserialize<UlongId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(ulongValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ULongAsString_Deserializes()
	{
		// Arrange
		var ulongValue = 987654321UL;
		var json = JsonSerializer.Serialize(ulongValue.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<UlongId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(ulongValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_ULongIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((UlongId?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<UlongId?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion

	#region UShort

	[Fact]
	public void Serialize_UShort_SerializedAsUShort()
	{
		// Arrange
		var id = UshortId.Create(123);
		// Act
		var strongIdJson = JsonSerializer.Serialize(id);
		var primitiveIdJson = JsonSerializer.Serialize(id.PrimitiveValue);
		// Assert
		Assert.Equal(primitiveIdJson, strongIdJson);
	}

	[Fact]
	public void Deserialize_UShort_Deserializes()
	{
		// Arrange
		var ushortValue = (ushort)456;
		var json = JsonSerializer.Serialize(ushortValue);
		// Act
		var strongId = JsonSerializer.Deserialize<UshortId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(ushortValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UShortAsString_Deserializes()
	{
		// Arrange
		var ushortValue = (ushort)456;
		var json = JsonSerializer.Serialize(ushortValue.ToString());
		// Act
		var strongId = JsonSerializer.Deserialize<UshortId>(json);
		// Assert
		Assert.NotNull(strongId);
		Assert.Equal(ushortValue, strongId.PrimitiveValue);
	}

	[Fact]
	public void Deserialize_UShortIsNull_Deserializes()
	{
		// Arrange
		var json = JsonSerializer.Serialize((UshortId?)null);
		// Act
		var strongId = JsonSerializer.Deserialize<UshortId?>(json);
		// Assert
		Assert.Null(strongId);
	}

	#endregion
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