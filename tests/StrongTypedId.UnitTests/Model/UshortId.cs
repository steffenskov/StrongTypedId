namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<UshortId, ushort>))]
[StrongTypedIdJsonConverter<UshortId, ushort>]
public class UshortId : StrongTypedId<UshortId, ushort>
{
	public UshortId(ushort primitiveValue) : base(primitiveValue)
	{
	}
}