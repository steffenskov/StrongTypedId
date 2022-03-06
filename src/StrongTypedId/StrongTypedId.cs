using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrongTypedId
{

	/// <Summary>
	/// Abstract baseclass to represent a strong typed id. Use it like this:
	/// public class UserId: StrongTypedId<UserId, Guid>
	/// </Summary>
	public abstract class StrongTypedId<TStrongTypedId, TPrimitiveId> : IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>
	where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
	where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>
	{
		/// <Summary>
		/// JsonConverter for serializing purely the underlying value. Use it like this:
		/// [JsonConverter(typeof(UserId.StrongIdJsonConverter))]
		/// public class UserId: StrongTypedId<UserId, Guid>
		/// </Summary>
		public class StrongTypedIdJsonConverter : JsonConverter<TStrongTypedId>
		{
			public override TStrongTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				var value = (TPrimitiveId)GeTValue(reader);
				return Create(value);
			}

			private object GeTValue(Utf8JsonReader reader)
			{
				return typeof(TPrimitiveId) switch
				{
					Type t when t == typeof(bool) => reader.GetBoolean(),
					Type t when t == typeof(Guid) => reader.GetGuid(),
					Type t when t == typeof(Int16) => reader.GetInt16(),
					Type t when t == typeof(Int32) => reader.GetInt32(),
					Type t when t == typeof(Int64) => reader.GetInt64(),
					Type t when t == typeof(UInt16) => reader.GetUInt16(),
					Type t when t == typeof(UInt32) => reader.GetUInt32(),
					Type t when t == typeof(UInt64) => reader.GetUInt64(),
					Type t when t == typeof(float) => reader.GetSingle(),
					Type t when t == typeof(double) => reader.GetDouble(),
					Type t when t == typeof(decimal) => reader.GetDecimal(),
					Type t when t == typeof(byte) => reader.GetByte(),
					Type t when t == typeof(sbyte) => reader.GetSByte(),
					_ => throw new NotSupportedException()
				};
			}

			public override void Write(Utf8JsonWriter writer, TStrongTypedId value, JsonSerializerOptions options)
			{
				writer.WriteStringValue(value.ToString());
			}
		}

		private static readonly object _ctorDelegateLock = new();
		private static readonly ConcurrentDictionary<Type, Func<TPrimitiveId, TStrongTypedId>> _ctors = new();

		/// <Summary>
		/// Creates a new instance of your strong typed id.
		/// If your actual value is a Guid, Guid.NewGuid() will be used, otherwise the value will be the default for the type.
		/// </Summary>
		public static TStrongTypedId New()
		{
			if (typeof(TPrimitiveId) == typeof(Guid))
				return Create((TPrimitiveId)(object)Guid.NewGuid());
			else
				return Create(default);
		}

		private static TStrongTypedId Create(TPrimitiveId value)
		{
			var ctor = GetOrCreateCtor<TStrongTypedId>();
			var instance = ctor!.Invoke(value);
			return instance;
		}

		private static Func<TPrimitiveId, TStrongTypedId> GetOrCreateCtor<T>()
		{
			var idType = typeof(TStrongTypedId);
			if (!_ctors.TryGetValue(idType, out var func))
			{
				var ctor = idType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(TPrimitiveId) }, null);
				_ctors[idType] = func = CreateDelegate(ctor!);
			}
			return func;
		}

		private static Func<TPrimitiveId, TStrongTypedId> CreateDelegate(ConstructorInfo constructor)
		{
			var constructorParam = constructor.GetParameters();

			// Create the dynamic method
			var method =
				new DynamicMethod(
					string.Format("{0}__{1}", constructor.DeclaringType!.Name, Guid.NewGuid().ToString().Replace("-", "")),
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
			return PrimitiveId.Equals(obj);
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

		public bool Equals(TPrimitiveId other)
		{
			return PrimitiveId.Equals(other);
		}

		public int CompareTo(object? obj)
		{
			return PrimitiveId.CompareTo(obj);
		}

		public static bool operator ==(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.Equals(b?.PrimitiveId) == true;
		}

		public static bool operator ==(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, TPrimitiveId? b)
		{
			return a?.Equals(b) == true;
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
			return a?.Equals(b?.PrimitiveId) != true;
		}

		public static bool operator !=(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, TPrimitiveId? b)
		{
			return a?.Equals(b) != true;
		}

		public static bool operator !=(TPrimitiveId? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
			return a?.Equals(b?.PrimitiveId) != true;
		}
	}
}