namespace StrongTypedId;

public abstract class StrongTypedEnum<TSelf, TEnum> : StrongTypedValue<TSelf, string>
	where TSelf : StrongTypedValue<TSelf, string>
	where TEnum : struct, Enum
{
	protected StrongTypedEnum(TEnum value) : base(value.ToString())
	{
		if (!Enum.IsDefined(value))
		{
			throw new ArgumentException($@"Value ""{value}"" is not defined in enum {typeof(TEnum).Name}", nameof(value));
		}
	}

	protected StrongTypedEnum(string value) : base(value)
	{
		if (!Enum.TryParse<TEnum>(value, out _))
		{
			throw new ArgumentException($@"Value ""{value}"" is not defined in enum {typeof(TEnum).Name}", nameof(value));
		}
	}

	public TEnum PrimitiveEnumValue => Enum.Parse<TEnum>(PrimitiveValue, true);

	public static IEnumerable<TSelf> GetValidValues()
	{
		return Enum.GetValues<TEnum>().Select(value => Create(value.ToString()));
	}
}