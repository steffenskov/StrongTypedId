using System;
using System.Reflection;
using System.Reflection.Emit;
using StrongTypedId.Collections;

namespace StrongTypedId
{
    /// <Summary>
    /// Abstract baseclass to represent a strong typed value. Use it like this:
    /// public class EmailAddress: StrongTypedValue&lt;EmailAddress, string&gt;
    /// </Summary>

    public abstract class StrongTypedValue<TSelf, TPrimitiveValue> : IComparable,
        IComparable<StrongTypedValue<TSelf, TPrimitiveValue>>, IComparable<TPrimitiveValue>,
        IEquatable<TSelf>
        where TSelf : StrongTypedValue<TSelf, TPrimitiveValue>
        where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
    {
        private static readonly LockedConcurrentDictionary<Type, Func<TPrimitiveValue, TSelf>> _constructors = new();

        public static TSelf Create(TPrimitiveValue value)
        {
            var ctor = GetOrCreateCtor();
            var instance = ctor.Invoke(value);
            return instance;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell",
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

        public TPrimitiveValue Value { get; }

        protected StrongTypedValue(TPrimitiveValue value)
        {
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is TSelf strongTyped)
            {
                return Value.Equals(strongTyped.Value);
            }

            return Value.Equals(obj);
        }

        public bool Equals(TSelf? other)
        {
            return Value.Equals(other is null ? null : other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString()!;
        }

        public int CompareTo(TPrimitiveValue? other)
        {
            return Value.CompareTo(other);
        }

        public int CompareTo(object? obj)
        {
            return Value.CompareTo(obj);
        }

        public int CompareTo(StrongTypedValue<TSelf, TPrimitiveValue>? other)
        {
            return Value.CompareTo(other is null ? null : other.Value);
        }

        public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            return a?.Value.Equals(b is null ? null : b.Value) == true;
        }

        public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
        {
            return a?.Value.Equals(b) == true;
        }

        public static bool operator ==(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.Equals(b is null ? null : b.Value) == true;
        }

        public static bool operator >(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.Value.CompareTo(b is null ? null : b.Value) > 0;
        }

        public static bool operator >(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
        {
            return a?.Value.CompareTo(b) > 0;
        }

        public static bool operator >(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.CompareTo(b is null ? null : b.Value) > 0;
        }

        public static bool operator <(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.Value.CompareTo(b is null ? null : b.Value) < 0;
        }

        public static bool operator <(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
        {
            return a?.Value.CompareTo(b) < 0;
        }

        public static bool operator <(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.CompareTo(b is null ? null : b.Value) < 0;
        }

        public static bool operator >=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.Value.CompareTo(b is null ? null : b.Value) >= 0;
        }

        public static bool operator >=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
        {
            return a?.Value.CompareTo(b) >= 0;
        }

        public static bool operator >=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.CompareTo(b is null ? null : b.Value) >= 0;
        }

        public static bool operator <=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.Value.CompareTo(b is null ? null : b.Value) <= 0;
        }

        public static bool operator <=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
        {
            return a?.Value.CompareTo(b) <= 0;
        }

        public static bool operator <=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.CompareTo(b is null ? null : b.Value) <= 0;
        }

        public static bool operator !=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            if (a is null && b is null)
            {
                return false;
            }

            return a?.Value.Equals(b is null ? null : b.Value) != true;
        }

        public static bool operator !=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
        {
            return a?.Value.Equals(b) != true;
        }

        public static bool operator !=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.Equals(b is null ? null : b.Value) != true;
        }
    }
}