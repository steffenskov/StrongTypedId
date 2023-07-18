using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongTypedId.Converters;

/// <Summary>
/// JsonConverter for System.Text.Json. It serializes purely the underlying value. Use it like this:
/// [StrongTypedIdJsonConverter&lt;UserId, Guid&gt;]
/// public class UserId: StrongTypedId&lt;UserId, Guid&gt;
/// </Summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class StrongTypedIdJsonConverterAttribute<TStrongTypedId, TPrimitiveId> : JsonConverterAttribute
    where TStrongTypedId: StrongTypedId<TStrongTypedId, TPrimitiveId> 
    where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>, IParsable<TPrimitiveId>
{
    public StrongTypedIdJsonConverterAttribute() : base(typeof(StrongTypedIdJsonConverter<TStrongTypedId, TPrimitiveId>))
    {
    }
}


internal class
    StrongTypedIdJsonConverter<TStrongTypedId, TPrimitiveId> : StrongTypedValueJsonConverter<TStrongTypedId,
        TPrimitiveId>
    where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
    where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
    IParsable<TPrimitiveId>
{
    public override TStrongTypedId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value is null)
            return null!;
        
        return StrongTypedId<TStrongTypedId, TPrimitiveId>.Parse(value);
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, TStrongTypedId value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString());
    }
}