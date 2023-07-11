using System;
using System.Text.Json.Serialization;

namespace StrongTypedId.Converters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class StrongTypedValueJsonConverterAttribute<TStrongTypedValue, TPrimitiveValue> : JsonConverterAttribute
where TStrongTypedValue: StrongTypedValue<TStrongTypedValue, TPrimitiveValue> 
where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
    public StrongTypedValueJsonConverterAttribute() : base(typeof(SystemTextJsonConverter<TStrongTypedValue, TPrimitiveValue>))
    {
    }
}