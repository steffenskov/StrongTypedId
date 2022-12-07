using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace StrongTypedId
{
	/// <Summary>
	/// Abstract baseclass to represent a strong typed id. Use it like this:
	/// public class UserId: StrongTypedId<UserId, Guid>
	/// </Summary>
	public abstract class StrongTypedId<TStrongTypedId, TPrimitiveId> : IComparable, IComparable<StrongTypedId<TStrongTypedId, TPrimitiveId>>, IComparable<TPrimitiveId>, IEquatable<TStrongTypedId>
		where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>
	{
		private static readonly object _ctorDelegateLock = new();
		private static readonly ConcurrentDictionary<Type, Func<TPrimitiveId, TStrongTypedId>> _ctors = new();

		/// <Summary>
		/// Creates a new instance of your strong typed id.
		/// If your actual value is a Guid, Guid.NewGuid() will be used, otherwise the value will be the default for the type.
		/// </Summary>
		public static TStrongTypedId New()
		{
			if (typeof(TPrimitiveId) == typeof(Guid))
			{
				return Create((TPrimitiveId)(object)Guid.NewGuid());
			}
			else
			{
				return Create(default);
			}
		}

		public static TStrongTypedId Create(TPrimitiveId value)
		{
			var ctor = GetOrCreateCtor();
			var instance = ctor!.Invoke(value);
			return instance;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
			Justification = "We know the ctor is protected and have control over this")]
		private static Func<TPrimitiveId, TStrongTypedId> GetOrCreateCtor()
		{
			var idType = typeof(TStrongTypedId);
			if (!_ctors.TryGetValue(idType, out var func))
			{
				lock (_ctorDelegateLock)
				{
					if (!_ctors.TryGetValue(idType, out func))
					{
						var ctor = idType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(TPrimitiveId) }, null);
						_ctors[idType] = func = CreateDelegate(ctor!);
					}
				}
			}

			return func;
		}

		private static Func<TPrimitiveId, TStrongTypedId> CreateDelegate(ConstructorInfo constructor)
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
			return (Func<TPrimitiveId, TStrongTypedId>)method.CreateDelegate(typeof(Func<TPrimitiveId, TStrongTypedId>));
		}

		public TPrimitiveId PrimitiveId { get; }

		protected StrongTypedId(TPrimitiveId primitiveId)
		{
			PrimitiveId = primitiveId;
		}

		public override bool Equals(object? obj)
		{
			if (obj is TStrongTypedId strongTyped)
			{
				return PrimitiveId.Equals(strongTyped.PrimitiveId);
			}

			return PrimitiveId.Equals(obj);
		}

		public bool Equals(TStrongTypedId? other)
		{
			return PrimitiveId.Equals(other?.PrimitiveId);
		}

		public override int GetHashCode()
		{
			return PrimitiveId.GetHashCode();
		}

		public override string ToString()
		{
			return PrimitiveId.ToString()!;
		}

		public int CompareTo(TPrimitiveId other)
		{
			return PrimitiveId.CompareTo(other);
		}

		public int CompareTo(object? obj)
		{
			return PrimitiveId.CompareTo(obj);
		}

		public int CompareTo(StrongTypedId<TStrongTypedId, TPrimitiveId>? other)
		{
			return PrimitiveId.CompareTo(other?.PrimitiveId);
		}

		public static bool operator ==(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			if (a is null && b is null)
			{
				return true;
			}

			return a?.PrimitiveId.Equals(b?.PrimitiveId) == true;
		}

		public static bool operator ==(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, TPrimitiveId b)
		{
			return a?.PrimitiveId.Equals(b) == true;
		}

		public static bool operator ==(TPrimitiveId? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.Equals(b?.PrimitiveId) == true;
		}

		public static bool operator >(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.PrimitiveId.CompareTo(b?.PrimitiveId) > 0;
		}

		public static bool operator >(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, TPrimitiveId? b)
		{
			return a?.PrimitiveId.CompareTo(b) > 0;
		}

		public static bool operator >(TPrimitiveId? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.CompareTo(b?.PrimitiveId) > 0;
		}

		public static bool operator <(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.PrimitiveId.CompareTo(b?.PrimitiveId) < 0;
		}

		public static bool operator <(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, TPrimitiveId? b)
		{
			return a?.PrimitiveId.CompareTo(b) < 0;
		}

		public static bool operator <(TPrimitiveId? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.CompareTo(b?.PrimitiveId) < 0;
		}

		public static bool operator >=(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.PrimitiveId.CompareTo(b?.PrimitiveId) >= 0;
		}

		public static bool operator >=(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, TPrimitiveId? b)
		{
			return a?.PrimitiveId.CompareTo(b) >= 0;
		}

		public static bool operator >=(TPrimitiveId? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.CompareTo(b?.PrimitiveId) >= 0;
		}

		public static bool operator <=(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.PrimitiveId.CompareTo(b?.PrimitiveId) <= 0;
		}

		public static bool operator <=(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, TPrimitiveId? b)
		{
			return a?.PrimitiveId.CompareTo(b) <= 0;
		}

		public static bool operator <=(TPrimitiveId? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.CompareTo(b?.PrimitiveId) <= 0;
		}

		public static bool operator !=(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			if (a is null && b is null)
			{
				return false;
			}

			return a?.PrimitiveId.Equals(b?.PrimitiveId) != true;
		}

		public static bool operator !=(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, TPrimitiveId b)
		{
			return a?.PrimitiveId.Equals(b) != true;
		}

		public static bool operator !=(TPrimitiveId? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.Equals(b?.PrimitiveId) != true;
		}
	}
}