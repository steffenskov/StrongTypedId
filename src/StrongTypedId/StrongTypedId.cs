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
	public abstract class StrongTypedId<TConcreteClass, TValue> : IComparable, IComparable<TValue>, IEquatable<TValue>
	where TConcreteClass : StrongTypedId<TConcreteClass, TValue>
	where TValue : struct, IComparable, IComparable<TValue>, IEquatable<TValue>
	{
		/// <Summary>
		/// JsonConverter for serializing purely the underlying value. Use it like this:
		/// [JsonConverter(typeof(UserId.StrongIdJsonConverter))]
		/// public class UserId: StrongTypedId<UserId, Guid>
		/// </Summary>
		public class StrongTypedIdJsonConverter : JsonConverter<TConcreteClass>
		{
			public override TConcreteClass Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				var value = (TValue)GeTValue(reader);
				return Create(value);
			}

			private object GeTValue(Utf8JsonReader reader)
			{
				return typeof(TValue) switch
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

			public override void Write(Utf8JsonWriter writer, TConcreteClass value, JsonSerializerOptions options)
			{
				writer.WriteStringValue(value.ToString());
			}
		}

		private static readonly object _ctorDelegateLock = new();
		private static readonly ConcurrentDictionary<Type, Func<TValue, TConcreteClass>> _ctors = new();

		/// <Summary>
		/// Creates a new instance of your strong typed id.
		/// If your actual value is a Guid, Guid.NewGuid() will be used, otherwise the value will be the default for the type.
		/// </Summary>
		public static TConcreteClass New()
		{
			if (typeof(TValue) == typeof(Guid))
				return Create((TValue)(object)Guid.NewGuid());
			else
				return Create(default);
		}

		private static TConcreteClass Create(TValue value)
		{
			var ctor = GetOrCreateCtor<TConcreteClass>();
			var instance = ctor!.Invoke(value);
			return instance;
		}

		private static Func<TValue, TConcreteClass> GetOrCreateCtor<T>()
		{
			var idType = typeof(TConcreteClass);
			if (!_ctors.TryGetValue(idType, out var func))
			{
				var ctor = idType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(TValue) }, null);
				_ctors[idType] = func = CreateDelegate(ctor!);
			}
			return func;
		}

		private static Func<TValue, TConcreteClass> CreateDelegate(ConstructorInfo constructor)
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
			return (Func<TValue, TConcreteClass>)method.CreateDelegate(typeof(Func<TValue, TConcreteClass>));
		}

		private TValue _value;

		protected StrongTypedId(TValue value)
		{
			_value = value;
		}

		public override bool Equals(object? obj)
		{
			return _value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		public override string ToString()
		{
			return _value.ToString()!;
		}

		public int CompareTo(TValue other)
		{
			return _value.CompareTo(other);
		}

		public bool Equals(TValue other)
		{
			return _value.Equals(other);
		}

		public int CompareTo(object? obj)
		{
			return _value.CompareTo(obj);
		}

		public static bool operator ==(StrongTypedId<TConcreteClass, TValue>? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?.Equals(b?._value) == true;
		}

		public static bool operator ==(StrongTypedId<TConcreteClass, TValue>? a, TValue? b)
		{
			return a?.Equals(b) == true;
		}

		public static bool operator ==(TValue? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?.Equals(b?._value) == true;
		}

		public static bool operator >(StrongTypedId<TConcreteClass, TValue>? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?._value.CompareTo(b?._value) > 0;
		}

		public static bool operator >(StrongTypedId<TConcreteClass, TValue>? a, TValue? b)
		{
			return a?._value.CompareTo(b) > 0;
		}

		public static bool operator >(TValue? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?.CompareTo(b?._value) > 0;
		}

		public static bool operator <(StrongTypedId<TConcreteClass, TValue>? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?._value.CompareTo(b?._value) < 0;
		}

		public static bool operator <(StrongTypedId<TConcreteClass, TValue>? a, TValue? b)
		{
			return a?._value.CompareTo(b) < 0;
		}

		public static bool operator <(TValue? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?.CompareTo(b?._value) < 0;
		}

		public static bool operator >=(StrongTypedId<TConcreteClass, TValue>? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?._value.CompareTo(b?._value) >= 0;
		}

		public static bool operator >=(StrongTypedId<TConcreteClass, TValue>? a, TValue? b)
		{
			return a?._value.CompareTo(b) >= 0;
		}

		public static bool operator >=(TValue? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?.CompareTo(b?._value) >= 0;
		}

		public static bool operator <=(StrongTypedId<TConcreteClass, TValue>? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?._value.CompareTo(b?._value) <= 0;
		}

		public static bool operator <=(StrongTypedId<TConcreteClass, TValue>? a, TValue? b)
		{
			return a?._value.CompareTo(b) <= 0;
		}

		public static bool operator <=(TValue? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?.CompareTo(b?._value) <= 0;
		}

		public static bool operator !=(StrongTypedId<TConcreteClass, TValue>? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?.Equals(b?._value) != true;
		}

		public static bool operator !=(StrongTypedId<TConcreteClass, TValue>? a, TValue? b)
		{
			return a?.Equals(b) != true;
		}

		public static bool operator !=(TValue? a, StrongTypedId<TConcreteClass, TValue>? b)
		{
			return a?.Equals(b?._value) != true;
		}
	}
}