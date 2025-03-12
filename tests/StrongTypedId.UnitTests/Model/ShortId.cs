namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<ShortId, short>))]
[StrongTypedIdJsonConverter<ShortId, short>]
public class ShortId : StrongTypedId<ShortId, short>
{
	public ShortId(short primitiveValue) : base(primitiveValue)
	{
	}
}