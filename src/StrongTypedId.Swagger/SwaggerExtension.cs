using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StrongTypedId;

namespace Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerExtension
{
    /// <summary>
    /// Map the strong type to its primitive type for Swagger.
    /// </summary>
    /// <typeparam name="TStrongType">Strong type to map</typeparam>
    /// <typeparam name="TPrimitiveType">Primitive type to map it to</typeparam>
    public static void MapStrongType<TStrongType, TPrimitiveType>(this SwaggerGenOptions options)
        where TStrongType : StrongTypedValue<TStrongType, TPrimitiveType>
        where TPrimitiveType : IComparable, IComparable<TPrimitiveType>, IEquatable<TPrimitiveType>
    {
        MapStrongType(options, typeof(TStrongType), typeof(TPrimitiveType));
    }

    /// <summary>
    /// Map the strong type to its primitive type for Swagger.
    /// </summary>
    /// <param name="strongType">Strong type to map</param>
    /// <param name="primitiveType">Primitive type to map it to</param>
    public static void MapStrongType(this SwaggerGenOptions options, Type strongType, Type primitiveType)
    {
        var (type, format) = GetOpenApiSchema(primitiveType);
        options.MapType(strongType, () => new OpenApiSchema { Type = type, Format = format });
    }

    private static (string type, string format) GetOpenApiSchema(Type primitiveType)
    {
        return primitiveType switch
        {
            { } t when t == typeof(bool) => ("boolean", ""),
            { } t when t == typeof(char) => ("string", ""),
            { } t when t == typeof(string) => ("string", ""),
            { } t when t == typeof(Guid) => ("string", "uuid"),
            { } t when t == typeof(short) => ("integer", "int16"),
            { } t when t == typeof(int) => ("integer", "int32"),
            { } t when t == typeof(long) => ("integer", "int64"),
            { } t when t == typeof(ushort) => ("integer", "uint16"),
            { } t when t == typeof(uint) => ("integer", "uint32"),
            { } t when t == typeof(ulong) => ("integer", "uint64"),
            { } t when t == typeof(float) => ("number", "float"),
            { } t when t == typeof(double) => ("number", "double"),
            { } t when t == typeof(decimal) => ("number", ""),
            { } t when t == typeof(byte) => ("integer", "uint8"),
            { } t when t == typeof(sbyte) => ("integer", "int8"),
            _ => ("object", "")
        };
    }
}