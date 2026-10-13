using System.Reflection;
using Microsoft.AspNetCore.OpenApi;

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
			var schemaId = GetSchemaId(strongType);

			var previous = options.CreateSchemaReferenceId;
			options.CreateSchemaReferenceId = typeInfo =>
				typeInfo.Type == strongType ? schemaId : previous(typeInfo);

			options.AddSchemaTransformer(async (schema, context, ct) =>
			{
				if (context.JsonTypeInfo.Type != strongType)
				{
					return;
				}

				var primitive = await context.GetOrCreateSchemaAsync(primitiveType, null, ct).ConfigureAwait(false);

				schema.Type = primitive.Type;
				schema.Format = primitive.Format;
				schema.Pattern = primitive.Pattern;
				schema.Minimum = primitive.Minimum;
				schema.Maximum = primitive.Maximum;
				schema.Properties = null;
				schema.Items = null;
				schema.AllOf = null;
				schema.AnyOf = null;
				schema.OneOf = null;
				schema.Required = null;
			});


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
		/// <param name="assembly">Assembly to scan</param>
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

	static internal string GetSchemaId(Type type)
	{
		return (type.FullName ?? type.Name).Replace('+', '.');
	}
}