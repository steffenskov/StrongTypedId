using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongTypedId.Converters;

internal class StrongTypedValueJsonConverterFactory : JsonConverterFactory
{
	// Small cache for converter instances - much more memory efficient than delegates
	private static readonly ConcurrentDictionary<Type, JsonConverter> _converterCache = new();

	public override bool CanConvert(Type typeToConvert)
	{
		return typeToConvert.IsStrongTypedValue();
	}

	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		return _converterCache.GetOrAdd(typeToConvert, CreateConverterForType);
	}

	private static JsonConverter CreateConverterForType(Type typeToConvert)
	{
		var (tSelf, tPrimitive) = typeToConvert.GetStrongTypedValueArguments();

		// Create the generic converter type

		var converterType = tSelf.IsStrongTypedId()
			? typeof(StrongTypedIdJsonConverter<,>).MakeGenericType(tSelf, tPrimitive)
			: typeof(StrongTypedValueJsonConverter<,>).MakeGenericType(tSelf, tPrimitive);

		// Create an instance of the generic converter
		var result = Activator.CreateInstance(converterType) as JsonConverter;
		return result ?? throw new UnreachableException("JsonConverter not created");
	}
}