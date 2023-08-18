using System;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using StrongTypedId.Collections;
using StrongTypedId.Converters;

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

        public TPrimitiveValue PrimitiveId { get; }

        protected StrongTypedValue(TPrimitiveValue primitiveId)
        {
            PrimitiveId = primitiveId;
        }

        public override bool Equals(object? obj)
        {
            if (obj is TSelf strongTyped)
            {
                return PrimitiveId.Equals(strongTyped.PrimitiveId);
            }

            return PrimitiveId.Equals(obj);
        }

        public bool Equals(TSelf? other)
        {
            return PrimitiveId.Equals(other is null ? null : other.PrimitiveId);
        }

        public override int GetHashCode()
        {
            return PrimitiveId.GetHashCode();
        }

        public override string ToString()
        {
            return PrimitiveId.ToString()!;
        }

        public int CompareTo(TPrimitiveValue? other)
        {
            return PrimitiveId.CompareTo(other);
        }

        public int CompareTo(object? obj)
        {
            return PrimitiveId.CompareTo(obj);
        }

        public int CompareTo(StrongTypedValue<TSelf, TPrimitiveValue>? other)
        {
            return PrimitiveId.CompareTo(other is null ? null : other.PrimitiveId);
        }

        public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            return a?.PrimitiveId.Equals(b is null ? null : b.PrimitiveId) == true;
        }

        public static bool operator ==(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
        {
            return a?.PrimitiveId.Equals(b) == true;
        }

        public static bool operator ==(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.Equals(b is null ? null : b.PrimitiveId) == true;
        }

        public static bool operator >(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.PrimitiveId.CompareTo(b is null ? null : b.PrimitiveId) > 0;
        }

        public static bool operator >(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
        {
            return a?.PrimitiveId.CompareTo(b) > 0;
        }

        public static bool operator >(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.CompareTo(b is null ? null : b.PrimitiveId) > 0;
        }

        public static bool operator <(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.PrimitiveId.CompareTo(b is null ? null : b.PrimitiveId) < 0;
        }

        public static bool operator <(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
        {
            return a?.PrimitiveId.CompareTo(b) < 0;
        }

        public static bool operator <(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.CompareTo(b is null ? null : b.PrimitiveId) < 0;
        }

        public static bool operator >=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.PrimitiveId.CompareTo(b is null ? null : b.PrimitiveId) >= 0;
        }

        public static bool operator >=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
        {
            return a?.PrimitiveId.CompareTo(b) >= 0;
        }

        public static bool operator >=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.CompareTo(b is null ? null : b.PrimitiveId) >= 0;
        }

        public static bool operator <=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.PrimitiveId.CompareTo(b is null ? null : b.PrimitiveId) <= 0;
        }

        public static bool operator <=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue? b)
        {
            return a?.PrimitiveId.CompareTo(b) <= 0;
        }

        public static bool operator <=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.CompareTo(b is null ? null : b.PrimitiveId) <= 0;
        }

        public static bool operator !=(StrongTypedValue<TSelf, TPrimitiveValue>? a,
            StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            if (a is null && b is null)
            {
                return false;
            }

            return a?.PrimitiveId.Equals(b is null ? null : b.PrimitiveId) != true;
        }

        public static bool operator !=(StrongTypedValue<TSelf, TPrimitiveValue>? a, TPrimitiveValue b)
        {
            return a?.PrimitiveId.Equals(b) != true;
        }

        public static bool operator !=(TPrimitiveValue? a, StrongTypedValue<TSelf, TPrimitiveValue>? b)
        {
            return a?.Equals(b is null ? null : b.PrimitiveId) != true;
        }
    }
}