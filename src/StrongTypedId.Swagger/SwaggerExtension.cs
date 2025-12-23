using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using StrongTypedId;

namespace Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerExtension
{
	/// <summary>
	///     Map the strong type to its primitive type for Swagger.
	/// </summary>
	/// <typeparam name="TStrongType">Strong type to map</typeparam>
	/// <typeparam name="TPrimitiveType">Primitive type to map it to</typeparam>
	public static void MapStrongType<TStrongType, TPrimitiveType>(this SwaggerGenOptions options)
		where TStrongType : IStrongTypedValue<TStrongType, TPrimitiveType>
		where TPrimitiveType : IComparable, IComparable<TPrimitiveType>, IEquatable<TPrimitiveType>
	{
		options.MapStrongType(typeof(TStrongType), typeof(TPrimitiveType));
	}

	/// <summary>
	///     Map the strong type to its primitive type for Swagger.
	/// </summary>
	/// <param name="strongType">Strong type to map</param>
	/// <param name="primitiveType">Primitive type to map it to</param>
	public static void MapStrongType(this SwaggerGenOptions options, Type strongType, Type primitiveType)
	{
		var (type, format) = GetOpenApiSchema(primitiveType);
		options.MapType(strongType, () => new OpenApiSchema { Type = type, Format = format });
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