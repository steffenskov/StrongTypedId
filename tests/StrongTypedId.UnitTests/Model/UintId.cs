namespace StrongTypedId.UnitTests.Model;

[TypeConverter(typeof(StrongTypedValueTypeConverter<UintId, uint>))]
[StrongTypedIdJsonConverter<UintId, uint>]
public class UintId : StrongTypedId<UintId, uint>
{
	public UintId(uint primitiveValue) : base(primitiveValue)
	{
	}
}