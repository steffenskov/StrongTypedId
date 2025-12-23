using Newtonsoft.Json;

namespace StrongTypedId.UnitTests.Model;

[StrongTypedValueJsonConverterFactory]
[JsonConverter(typeof(NewtonSoftJsonConverter<AttributedGuidId, Guid>))]
public class AttributedGuidId : StrongTypedGuid<AttributedGuidId>
{
	public AttributedGuidId(Guid primitiveValue) : base(primitiveValue)
	{
	}
}

[StrongTypedValueJsonConverterFactory]
public struct GuidId : IStrongTypedGuid<GuidId>
{
	public GuidId(Guid primitiveId)
	{
		PrimitiveValue = primitiveId;
	}

	public Guid PrimitiveValue { get; }

	public bool Equals(GuidId other)
	{
		return PrimitiveValue.Equals(other.PrimitiveValue);
	}

	public override bool Equals(object? obj)
	{
		return obj is GuidId other && Equals(other);
	}

	public override int GetHashCode()
	{
		return PrimitiveValue.GetHashCode();
	}

	public override string ToString()
	{
		return PrimitiveValue.ToString();
	}
}