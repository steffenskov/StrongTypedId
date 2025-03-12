namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<FloatValue, float>))]
[StrongTypedValueJsonConverter<FloatValue, float>]
public class FloatValue : StrongTypedValue<FloatValue, float>
{
	public FloatValue(float primitiveValue) : base(primitiveValue)
	{
	}
}