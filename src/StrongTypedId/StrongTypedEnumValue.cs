namespace StrongTypedId;

public abstract class StrongTypedEnumValue<TSelf, TEnum> : StrongTypedValue<TSelf, string>
	where TSelf : StrongTypedValue<TSelf, string>
	where TEnum : struct, Enum
{
	protected StrongTypedEnumValue(TEnum value) : base(value.ToString())
	{
		if (!Enum.IsDefined(value))
		{
			throw new ArgumentException($@"Value ""{value}"" is not defined in enum {typeof(TEnum).Name}", nameof(value));
		}
	}

	protected StrongTypedEnumValue(string value) : this(Parse(value))
	{
	}

	public TEnum PrimitiveEnumValue => Enum.Parse<TEnum>(PrimitiveValue, true);

	private static TEnum Parse(string value)
	{
		return Enum.TryParse<TEnum>(value, out var result)
			? result
			: throw new ArgumentException($@"Value ""{value}"" is not defined in enum {typeof(TEnum).Name}", nameof(value));
	}

	public static IEnumerable<TSelf> GetValidValues()
	{
		return Enum.GetValues<TEnum>().Select(value => Create(value.ToString()));
	}
}