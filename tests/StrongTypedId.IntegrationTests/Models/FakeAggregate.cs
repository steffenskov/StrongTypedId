namespace StrongTypedId.IntegrationTests.Models;

public record FakeAggregate(FakeId Id) : IAggregate<FakeId>
{
	public StrongBoolId BoolId { get; init; } = default!;
	public StrongBoolValue BoolValue { get; init; } = default!;
	public StrongDecimalId DecimalId { get; init; } = default!;
	public StrongDecimalValue DecimalValue { get; init; } = default!;
	public StrongDoubleId DoubleId { get; init; } = default!;
	public StrongDoubleValue DoubleValue { get; init; } = default!;
	public StrongGuidId GuidId { get; init; } = default!;
	public StrongGuidValue GuidValue { get; init; } = default!;
	public StrongIntId IntId { get; init; } = default!;
	public StrongIntValue IntValue { get; init; } = default!;
	public StrongLongId LongId { get; init; } = default!;
	public StrongLongValue LongValue { get; init; } = default!;
	public StrongStringValue StringValue { get; init; } = default!;
	public StrongEnumValue EnumValue { get; init; } = default!;
}