using System.Text.Json.Serialization;

namespace StrongTypedId.Converters;

/// <Summary>
///     JsonConverter for System.Text.Json. It serializes purely the underlying value. Use it like this:
///     [StrongTypedValueJsonConverterFactory]
///     public class EmailAddress: StrongTypedValue&lt;EmailAddress, string&gt;
/// </Summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class StrongTypedValueJsonConverterFactoryAttribute : JsonConverterAttribute
{
	public StrongTypedValueJsonConverterFactoryAttribute() : base(typeof(StrongTypedValueJsonConverterFactory))
	{
	}
}