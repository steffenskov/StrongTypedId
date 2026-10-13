using System.Reflection;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace StrongTypedId.OpenApi;

public static class OpenApiStrongTypeExtension
{
	extension(OpenApiOptions options)
	{
		/// <summary>
		///     Map the strong type to its primitive type in the generated OpenAPI document.
		/// </summary>
		public OpenApiOptions MapStrongType<TStrongType, TPrimitiveType>()
			where TStrongType : StrongTypedValue<TStrongType, TPrimitiveType>
			where TPrimitiveType : IComparable, IComparable<TPrimitiveType>, IEquatable<TPrimitiveType>
		{
			options.MapStrongType(typeof(TStrongType), typeof(TPrimitiveType));

			return options;
		}

		/// <summary>
		///     Map the strong type to its primitive type in the generated OpenAPI document.
		/// </summary>
		public OpenApiOptions MapStrongType(Type strongType, Type primitiveType)
		{
			options.AddDocumentTransformer((doc, context, ct) => ReplaceSchema(doc, strongType, primitiveType));
			return options;
		}

		/// <summary>
		///     Scan the assembly T belongs to for StrongTyped types and map them.
		/// </summary>
		/// <typeparam name="T">Marker type for assembly to scan</typeparam>
		public OpenApiOptions MapStrongTypesFromAssemblyContaining<T>()
		{
			options.MapStrongTypesFromAssemblyContaining(typeof(T));
			return options;
		}

		/// <summary>
		///     Scan the assembly markerType belongs to for StrongTyped types and map them.
		/// </summary>
		/// <param name="markerType">Marker type for assembly to scan</param>
		public OpenApiOptions MapStrongTypesFromAssemblyContaining(Type markerType)
		{
			ArgumentNullException.ThrowIfNull(markerType);
			options.MapStrongTypesFromAssembly(markerType.Assembly);
			return options;
		}

		/// <summary>
		///     Scan assemblies for StrongTyped types and map them.
		/// </summary>
		/// <param name="assemblies">Assemblies to scan</param>
		public OpenApiOptions MapStrongTypesFromAssemblies(IEnumerable<Assembly> assemblies)
		{
			ArgumentNullException.ThrowIfNull(assemblies);
			foreach (var assembly in assemblies)
			{
				options.MapStrongTypesFromAssembly(assembly);
			}

			return options;
		}

		/// <summary>
		///     Scan assembly for StrongTyped types and map them.
		/// </summary>
		/// <param name="assembly">Asembly to scan</param>
		public OpenApiOptions MapStrongTypesFromAssembly(Assembly assembly)
		{
			ArgumentNullException.ThrowIfNull(assembly);
			foreach (var type in assembly.GetTypes())
			{
				if (type.IsAbstract || type.IsInterface)
				{
					continue;
				}

				if (!type.IsStrongTypedValue())
				{
					continue;
				}

				var (strongType, primitiveType) = type.GetStrongTypedValueArguments();
				options.MapStrongType(strongType, primitiveType);
			}

			return options;
		}
	}

	private static Task ReplaceSchema(OpenApiDocument doc, Type strongType, Type primitiveType)
	{
		var (jsonType, format) = GetOpenApiSchema(primitiveType);

		// The built-in generator names components by the CLR type name (e.g. "OrderId").
		var componentKey = strongType.Name;

		if (doc.Components?.Schemas is null)
		{
			return Task.CompletedTask;
		}

		doc.Components.Schemas[componentKey] = new OpenApiSchema
		{
			Type = jsonType,
			Format = format,
			Properties = null,
			Items = null,
			AllOf = null,
			AnyOf = null,
			OneOf = null,
			Required = null
		};

		return Task.CompletedTask;
	}

	private static (JsonSchemaType type, string format) GetOpenApiSchema(Type primitiveType)
	{
		return primitiveType switch
		{
			{ } t when t == typeof(bool) => (JsonSchemaType.Boolean, ""),
			{ } t when t == typeof(char) => (JsonSchemaType.String, ""),
			{ } t when t == typeof(string) => (JsonSchemaType.String, ""),
			{ } t when t == typeof(Guid) => (JsonSchemaType.String, "uuid"),
			{ } t when t == typeof(short) => (JsonSchemaType.Integer, "int16"),
			{ } t when t == typeof(int) => (JsonSchemaType.Integer, "int32"),
			{ } t when t == typeof(long) => (JsonSchemaType.Integer, "int64"),
			{ } t when t == typeof(ushort) => (JsonSchemaType.Integer, "uint16"),
			{ } t when t == typeof(uint) => (JsonSchemaType.Integer, "uint32"),
			{ } t when t == typeof(ulong) => (JsonSchemaType.Integer, "uint64"),
			{ } t when t == typeof(float) => (JsonSchemaType.Number, "float"),
			{ } t when t == typeof(double) => (JsonSchemaType.Number, "double"),
			{ } t when t == typeof(decimal) => (JsonSchemaType.Number, ""),
			{ } t when t == typeof(byte) => (JsonSchemaType.Integer, "uint8"),
			{ } t when t == typeof(sbyte) => (JsonSchemaType.Integer, "int8"),
			_ => (JsonSchemaType.Object, "")
		};
	}
}