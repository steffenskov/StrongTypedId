using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace StrongTypedId.Converters;

public class StrongTypedNewtonSoftJsonConverter : JsonConverter
{
	public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
	{
		if (value is null)
		{
			writer.WriteNull();
			return;
		}

		if (serializer.TypeNameHandling is TypeNameHandling.All or TypeNameHandling.Objects or TypeNameHandling.Auto)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("$type");
			writer.WriteValue(FormatType(value));
			writer.WritePropertyName("Value");
			writer.WriteValue(value.ToString());
			writer.WriteEndObject();
		}
		else
		{
			serializer.Serialize(writer, ((IStrongTypedValue)value).PrimitiveValue);
		}
	}

	private static string FormatType(object value)
	{
		var result = value.GetType().AssemblyQualifiedName ?? throw new InvalidOperationException($"Cannot retrieve AssemblyQualifiedName of type: {value.GetType().Name}");
		var index = result.IndexOf(", Version", StringComparison.InvariantCulture);
		if (index >= 0)
		{
			return result.Remove(index);
		}

		return result;
	}

	public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			return null;
		}

		if (reader.TokenType == JsonToken.StartObject)
		{
			var props = ReadTokens(reader);

			var type = Type.GetType(props["$type"]) ?? throw new InvalidOperationException("Cannot instantiate type: " + props["$type"]);
			var value = props["Value"];

			return StrongTypedValue.Create(type, value);
		}

		if (objectType.IsInterface || objectType.IsAbstract)
		{
			throw new InvalidOperationException("Cannot deserialize interface or abstract type when JSON doesn't contain $type. Target type: " + objectType.Name);
		}

		var rawValue = GetRawValue(reader, objectType);

		var baseType = GetBaseType(objectType);
		var genericArguments = baseType.GetGenericArguments();
		var primitiveType = genericArguments[1];
		var typedValue = Convert.ChangeType(rawValue, primitiveType);

		return StrongTypedValue.Create(objectType, typedValue);
	}

	private static object GetRawValue(JsonReader reader, Type objectType)
	{
		return reader.TokenType switch
		{
			JsonToken.Integer => (long)reader.Value!,
			JsonToken.Boolean => (bool)reader.Value!,
			JsonToken.Float => (double)reader.Value!,
			JsonToken.Date => (DateTime)reader.Value!,
			JsonToken.String => objectType.IsStrongTypedGuid()
				? Guid.Parse((string)reader.Value!)
				: (string)reader.Value!,
			_ => throw new SwitchExpressionException($"Missing arm for tokenType: {reader.TokenType}")
		};
	}

	private static Type GetBaseType(Type type)
	{
		while (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(StrongTypedValue<,>))
		{
			return GetBaseType(type.BaseType ?? throw new InvalidOperationException("Type does not inherit from StrongTypedValue<,>"));
		}

		return type;
	}

	private static Dictionary<string, string> ReadTokens(JsonReader reader)
	{
		var key = "";
		Dictionary<string, string> result = [];
		while (reader.Read())
		{
			if (reader.TokenType == JsonToken.PropertyName)
			{
				key = reader.Value!.ToString()!;
			}
			else if (reader.TokenType == JsonToken.String)
			{
				var value = reader.Value!.ToString()!;
				result[key] = value;
			}
			else if (reader.TokenType == JsonToken.EndObject)
			{
				break;
			}
			else
			{
				throw new InvalidOperationException($"Unexpected token type: {reader.TokenType}");
			}
		}

		return result;
	}

	public override bool CanConvert(Type objectType)
	{
		return objectType.IsStrongTypedValue();
	}
}