namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<UlongId, ulong>))]
[StrongTypedIdJsonConverter<UlongId, ulong>]
public class UlongId : StrongTypedId<UlongId, ulong>
{
	public UlongId(ulong primitiveValue) : base(primitiveValue)
	{
	}
}