namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<DecimalValue, decimal>))]
[StrongTypedValueJsonConverter<DecimalValue, decimal>]
public class DecimalValue : StrongTypedValue<DecimalValue, decimal>
{
	public DecimalValue(decimal primitiveValue) : base(primitiveValue)
	{
	}
}