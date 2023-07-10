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
    public abstract class StrongTypedValue<TStrongTypedValue, TPrimitiveId> : IComparable,
        IComparable<StrongTypedValue<TStrongTypedValue, TPrimitiveId>>, IComparable<TPrimitiveId>,
        IEquatable<TStrongTypedValue>,
        IParsable<TStrongTypedValue?>
        where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, TPrimitiveId>
        where TPrimitiveId : IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>,
        IParsable<TPrimitiveId>
    {
        private static readonly LockedConcurrentDictionary<Type, Func<TPrimitiveId, TStrongTypedValue>> _constructors = new();

        public static TStrongTypedValue Create(TPrimitiveId value)
        {
            var ctor = GetOrCreateCtor();
            var instance = ctor.Invoke(value);
            return instance;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell",
            "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
            Justification = "We know the ctor is protected and have control over this")]
        private static Func<TPrimitiveId, TStrongTypedValue> GetOrCreateCtor()
        {
            var idType = typeof(TStrongTypedValue);
            return _constructors.GetOrAdd(idType, type =>
            {
                var ctor = type.GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
                    new[] { typeof(TPrimitiveId) }, null);
                return CreateDelegate(ctor ?? throw new InvalidOperationException(
                    $"No constructor found for type {type.Name} with one argument of type {typeof(TPrimitiveId).Name}."));
            });
        }

        private static Func<TPrimitiveId, TStrongTypedValue> CreateDelegate(ConstructorInfo constructor)
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
            return (Func<TPrimitiveId, TStrongTypedValue>)method.CreateDelegate(
                typeof(Func<TPrimitiveId, TStrongTypedValue>));
        }

        public TPrimitiveId PrimitiveId { get; }

        protected StrongTypedValue(TPrimitiveId primitiveId)
        {
            PrimitiveId = primitiveId;
        }

        public override bool Equals(object? obj)
        {
            if (obj is TStrongTypedValue strongTyped)
            {
                return PrimitiveId.Equals(strongTyped.PrimitiveId);
            }

            return PrimitiveId.Equals(obj);
        }

        public bool Equals(TStrongTypedValue? other)
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

        public int CompareTo(TPrimitiveId? other)
        {
            return PrimitiveId.CompareTo(other);
        }

        public int CompareTo(object? obj)
        {
            return PrimitiveId.CompareTo(obj);
        }

        public int CompareTo(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? other)
        {
            return PrimitiveId.CompareTo(other is null ? null : other.PrimitiveId);
        }

        public static bool operator ==(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a,
            StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            return a?.PrimitiveId.Equals(b is null ? null : b.PrimitiveId) == true;
        }

        public static bool operator ==(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a, TPrimitiveId b)
        {
            return a?.PrimitiveId.Equals(b) == true;
        }

        public static bool operator ==(TPrimitiveId? a, StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.Equals(b is null ? null : b.PrimitiveId) == true;
        }

        public static bool operator >(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a,
            StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.PrimitiveId.CompareTo(b is null ? null : b.PrimitiveId) > 0;
        }

        public static bool operator >(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a, TPrimitiveId? b)
        {
            return a?.PrimitiveId.CompareTo(b) > 0;
        }

        public static bool operator >(TPrimitiveId? a, StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.CompareTo(b is null ? null : b.PrimitiveId) > 0;
        }

        public static bool operator <(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a,
            StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.PrimitiveId.CompareTo(b is null ? null : b.PrimitiveId) < 0;
        }

        public static bool operator <(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a, TPrimitiveId? b)
        {
            return a?.PrimitiveId.CompareTo(b) < 0;
        }

        public static bool operator <(TPrimitiveId? a, StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.CompareTo(b is null ? null : b.PrimitiveId) < 0;
        }

        public static bool operator >=(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a,
            StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.PrimitiveId.CompareTo(b is null ? null : b.PrimitiveId) >= 0;
        }

        public static bool operator >=(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a, TPrimitiveId? b)
        {
            return a?.PrimitiveId.CompareTo(b) >= 0;
        }

        public static bool operator >=(TPrimitiveId? a, StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.CompareTo(b is null ? null : b.PrimitiveId) >= 0;
        }

        public static bool operator <=(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a,
            StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.PrimitiveId.CompareTo(b is null ? null : b.PrimitiveId) <= 0;
        }

        public static bool operator <=(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a, TPrimitiveId? b)
        {
            return a?.PrimitiveId.CompareTo(b) <= 0;
        }

        public static bool operator <=(TPrimitiveId? a, StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.CompareTo(b is null ? null : b.PrimitiveId) <= 0;
        }

        public static bool operator !=(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a,
            StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            if (a is null && b is null)
            {
                return false;
            }

            return a?.PrimitiveId.Equals(b is null ? null : b.PrimitiveId) != true;
        }

        public static bool operator !=(StrongTypedValue<TStrongTypedValue, TPrimitiveId>? a, TPrimitiveId b)
        {
            return a?.PrimitiveId.Equals(b) != true;
        }

        public static bool operator !=(TPrimitiveId? a, StrongTypedValue<TStrongTypedValue, TPrimitiveId>? b)
        {
            return a?.Equals(b is null ? null : b.PrimitiveId) != true;
        }

        public static TStrongTypedValue Parse(string s, IFormatProvider? provider = null)
        {
            return Create(TPrimitiveId.Parse(s, provider));
        }

        public static bool TryParse(string? s, out TStrongTypedValue? result)
        {
            return TryParse(s, null, out result);
        }

        public static bool TryParse(string? s, IFormatProvider? provider, out TStrongTypedValue? result)
        {
            if (TPrimitiveId.TryParse(s, provider, out var primitiveId))
            {
                result = Create(primitiveId);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }
}