using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using StrongTypedId.Collections;

namespace StrongTypedId;

/// <Summary>
///     Abstract baseclass to represent a strong typed value. Use it like this:
///     public class EmailAddress: StrongTypedValue&lt;EmailAddress, string&gt;
/// </Summary>
public abstract class StrongTypedValue<TSelf, TPrimitiveValue> : IComparable,
	IComparable<StrongTypedValue<TSelf, TPrimitiveValue>>, IComparable<TPrimitiveValue>,
	IEquatable<TSelf>, IStrongTypedValue<TPrimitiveValue>
	where TSelf : StrongTypedValue<TSelf, TPrimitiveValue>
	where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
	private static readonly LockedConcurrentDictionary<Type, Func<TPrimitiveValue, TSelf>> _constructors = new();

	protected StrongTypedValue(TPrimitiveValue primitiveValue)
	{
		PrimitiveValue = primitiveValue;
	}

	public int CompareTo(object? obj)
	{
		return PrimitiveValue.CompareTo(obj);
	}

	public int CompareTo(StrongTypedValue<TSelf, TPrimitiveValue>? other)
	{
		return PrimitiveValue.CompareTo(other is null ? null : other.PrimitiveValue);
	}

	public int CompareTo(TPrimitiveValue? other)
	{
		return PrimitiveValue.CompareTo(other);
	}

	public bool Equals(TSelf? other)
	{
		return PrimitiveValue.Equals(other is null ? null : other.PrimitiveValue);
	}

	public TPrimitiveValue PrimitiveValue { get; }

	public static TSelf Create(TPrimitiveValue value)
	{
		var ctor = GetOrCreateCtor();
		var instance = ctor.Invoke(value);
		return instance;
	}

	[SuppressMessage("Major Code Smell",
		"S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
		Justification = "We know the ctor is protected and have control over this")]
	private static Func<TPrimitiveValue, TSelf> GetOrCreateCtor()
	{
		var idType = typeof(TSelf);
		return _constructors.GetOrAdd(idType, type =>
		{
			var ctor = type.GetConstructor(
				BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
				new[] { typeof(TPrimitiveValue) }, null);
			return CreateDelegate(ctor ?? throw new InvalidOperationException(
				$"No constructor found for type {type.Name} with one argument of type {typeof(TPrimitiveValue).Name}."));
		});
	}

	private static Func<TPrimitiveValue, TSelf> CreateDelegate(ConstructorInfo constructor)
	{
		var constructorParam = constructor.GetParameters();

		// Create the dynamic method
		var method =
			new DynamicMethod($"{constructor.DeclaringType!.Name}__{Guid.NewGuid().ToString().Replace("-", "")}",
				constructor.DeclaringType,
				Array.ConvertAll<ParameterInfo, Type>(constructorParam, p => p.ParameterType),
				true
			);

		// Create the il
		var gen = method.GetILGenerator();
		gen.Emit(OpCodes.Ldarg_0);
		gen.Emit(OpCodes.Newobj, constructor);
		gen.Emit(OpCodes.Ret);

		// Return the delegate :)
		return (Func<TPrimitiveValue, TSelf>)method.CreateDelegate(
			typeof(Func<TPrimitiveValue, TSelf>));
	}

	public override bool Equals(object? obj)
	{
		if (obj is TSelf strongTyped)
		{
			return PrimitiveValue.Equals(strongTyped.PrimitiveValue);
		}

		return PrimitiveValue.Equals(obj);
	}

	public override int GetHashCode()
	{
		return PrimitiveValue.GetHashCode();
	}

	public override string ToString()
	{
		return PrimitiveValue.ToString()!;
	}

	public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		if (a is null && b is null)
		{
			return true;
		}

		return a?.PrimitiveValue.Equals(b is null ? null : b.PrimitiveValue) == true;
	}

	public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
	{
		return a?.PrimitiveValue.Equals(b) == true;
	}

	public static bool operator ==(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.Equals(b is null ? null : b.PrimitiveValue) == true;
	}

	public static bool operator >(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) > 0;
	}

	public static bool operator >(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) > 0;
	}

	public static bool operator >(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) > 0;
	}

	public static bool operator <(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) < 0;
	}

	public static bool operator <(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) < 0;
	}

	public static bool operator <(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) < 0;
	}

	public static bool operator >=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) >= 0;
	}

	public static bool operator >=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) >= 0;
	}

	public static bool operator >=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) >= 0;
	}

	public static bool operator <=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.PrimitiveValue.CompareTo(b is null ? null : b.PrimitiveValue) <= 0;
	}

	public static bool operator <=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
	{
		return a?.PrimitiveValue.CompareTo(b) <= 0;
	}

	public static bool operator <=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.CompareTo(b is null ? null : b.PrimitiveValue) <= 0;
	}

	public static bool operator !=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
		StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		if (a is null && b is null)
		{
			return false;
		}

		return a?.PrimitiveValue.Equals(b is null ? null : b.PrimitiveValue) != true;
	}

	public static bool operator !=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
	{
		return a?.PrimitiveValue.Equals(b) != true;
	}

	public static bool operator !=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
	{
		return a?.Equals(b is null ? null : b.PrimitiveValue) != true;
	}
}