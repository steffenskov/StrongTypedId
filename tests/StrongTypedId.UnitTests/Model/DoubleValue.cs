namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<DoubleValue, double>))]
[StrongTypedValueJsonConverter<DoubleValue, double>]
public class DoubleValue : StrongTypedValue<DoubleValue, double>
{
	public DoubleValue(double primitiveValue) : base(primitiveValue)
	{
	}
}