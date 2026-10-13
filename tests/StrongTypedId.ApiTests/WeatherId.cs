using Newtonsoft.Json;
using StrongTypedId.Converters;

namespace StrongTypedId.ApiTests;

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<WeatherId, Guid>))]
public class WeatherId : StrongTypedGuid<WeatherId>
{
	public WeatherId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}