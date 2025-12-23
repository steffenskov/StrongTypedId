namespace StrongTypedId.IntegrationTests.Models;

public record FakeAggregate(FakeId Id) : IAggregate<FakeId>
{
	public StrongBoolId BoolId { get; init; } = null!;
	public StrongBoolValue BoolValue { get; init; } = null!;
	public StrongDecimalId DecimalId { get; init; } = null!;
	public StrongDecimalValue DecimalValue { get; init; } = null!;
	public StrongDoubleId DoubleId { get; init; } = null!;
	public StrongDoubleValue DoubleValue { get; init; } = null!;
	public StrongGuidId GuidId { get; init; } = null!;
	public StrongGuidValue GuidValue { get; init; } = null!;
	public StrongIntId IntId { get; init; } = null!;
	public StrongIntValue IntValue { get; init; } = null!;
	public StrongLongId LongId { get; init; } = null!;
	public StrongLongValue LongValue { get; init; } = null!;
	public StrongStringValue StringValue { get; init; } = null!;
	public StrongEnumValue EnumValue { get; init; } = null!;
}