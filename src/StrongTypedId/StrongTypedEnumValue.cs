namespace StrongTypedId;

public abstract class StrongTypedEnumValue<TSelf, TEnum> : StrongTypedValue<TSelf, string>, IStrongTypedEnumValue<TSelf, TEnum>
	where TSelf : StrongTypedValue<TSelf, string>, IStrongTypedEnumValue<TSelf, TEnum>
	where TEnum : struct, Enum
{
	protected StrongTypedEnumValue(TEnum value) : base(value.ToString())
	{
		if (!Enum.IsDefined(value))
		{
			throw new ArgumentException($@"Value ""{value}"" is not defined in enum {typeof(TEnum).Name}", nameof(value));
		}
	}

	protected StrongTypedEnumValue(string value) : base(value)
	{
		if (!Enum.TryParse<TEnum>(value, out _))
		{
			throw new ArgumentException($@"Value ""{value}"" is not defined in enum {typeof(TEnum).Name}", nameof(value));
		}
	}
}

public interface IStrongTypedEnumValue<TSelf, TEnum> : IStrongTypedValue<TSelf, string>
	where TSelf : IStrongTypedEnumValue<TSelf, TEnum>
	where TEnum : struct, Enum
{
}