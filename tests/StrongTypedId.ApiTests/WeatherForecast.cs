namespace StrongTypedId.ApiTests;

internal record WeatherForecast(WeatherId Id, DateOnly Date, WrappedDecimal TemperatureC, string? Summary)
{
	public decimal TemperatureF => 32 + TemperatureC / 0.5556m;

	public TimeSpan TimeSpan { get; }
	public TimeOnly TimeOnly { get; }
	public WrappedDateTime DateTime { get; } = default!;
	public DateTimeOffset DateTimeOffset { get; }
}

public class WrappedDateTime : StrongTypedValue<WrappedDateTime, DateTime>
{
	public WrappedDateTime(DateTime primitiveValue) : base(primitiveValue)
	{
	}
}

public class WrappedDecimal : StrongTypedValue<WrappedDecimal, decimal>
{
	public WrappedDecimal(decimal primitiveValue) : base(primitiveValue)
	{
	}

	public static implicit operator decimal(WrappedDecimal val)
	{
		return val.PrimitiveValue;
	}

	public static implicit operator WrappedDecimal(decimal val)
	{
		return new WrappedDecimal(val);
	}
}