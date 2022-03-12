using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
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
	public abstract class StrongTypedId<TStrongTypedId, TPrimitiveId> : IComparable, IComparable<TPrimitiveId>, IEquatable<TStrongTypedId>
		where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>
	{
		/// <Summary>
		/// JsonConverter for System.Text.Json. It serializes purely the underlying value. Use it like this:
		/// [JsonConverter(typeof(UserId.StrongIdJsonConverter))]
		/// public class UserId: StrongTypedId<UserId, Guid>
		/// </Summary>
		public class StrongTypedIdJsonConverter : JsonConverter<TStrongTypedId>
		{
			public override TStrongTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				var value = (TPrimitiveId)GetValue(reader);
				return Create(value);
			}

			private static object GetValue(Utf8JsonReader reader)
			{
				return typeof(TPrimitiveId) switch
				{
					Type t when t == typeof(bool) => reader.GetBoolean(),
					Type t when t == typeof(Guid) => reader.GetGuid(),
					Type t when t == typeof(short) => reader.GetInt16(),
					Type t when t == typeof(int) => reader.GetInt32(),
					Type t when t == typeof(long) => reader.GetInt64(),
					Type t when t == typeof(ushort) => reader.GetUInt16(),
					Type t when t == typeof(uint) => reader.GetUInt32(),
					Type t when t == typeof(ulong) => reader.GetUInt64(),
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

		/// <summary>
		/// JsonConverter for Newtonsoft. It serializes purely the underlying value. Use it like this:
		/// [JsonConverter(typeof(UserId.StrongTypedIdNewtonSoftJsonConverter))]
		/// public class UserId: StrongTypedId<UserId, Guid>
		/// </summary>
		/*
		public class StrongTypedIdNewtonSoftJsonConverter : NewtonSoft.Json.JsonConverter<TStrongTypedId>
		{
			public override TStrongTypedId? ReadJson(JsonReader reader, Type objectType, TStrongTypedId? existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
			{
				var result = serializer.Deserialize<TPrimitiveId?>(reader);
				return result.HasValue
					? Create(result.Value)
					: null;
			}

			public override void WriteJson(JsonWriter writer, TStrongTypedId? value, Newtonsoft.Json.JsonSerializer serializer)
			{
				serializer.Serialize(writer, value?.PrimitiveId);
			}
		}*/

		/// <summary>
		/// TypeConverter for WebAPI and MVC. Its purpose is to allow StrongTypedIds as arguments to controller actions.
		/// Use it like this:
		/// [TypeConverter(typeof(UserId.StrongTypedIdTypeConverter))]
		/// public class UserId: StrongTypedId<UserId, Guid>
		/// </summary>
		public class StrongTypedIdTypeConverter : TypeConverter
		{
			public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
			{
				var stringValue = value as string;
				if (!string.IsNullOrEmpty(stringValue) && TryParse(stringValue, out var primitiveId))
				{
					return Create((TPrimitiveId)primitiveId);
				}

				return base.ConvertFrom(context, culture, value);
			}

			private static bool TryParse(string stringValue, out object primitiveId)
			{
				primitiveId = typeof(TPrimitiveId) switch
				{
					Type t when t == typeof(bool) => bool.Parse(stringValue),
					Type t when t == typeof(Guid) => Guid.Parse(stringValue),
					Type t when t == typeof(short) => short.Parse(stringValue),
					Type t when t == typeof(int) => int.Parse(stringValue),
					Type t when t == typeof(long) => long.Parse(stringValue),
					Type t when t == typeof(ushort) => ushort.Parse(stringValue),
					Type t when t == typeof(uint) => uint.Parse(stringValue),
					Type t when t == typeof(ulong) => ulong.Parse(stringValue),
					Type t when t == typeof(float) => float.Parse(stringValue),
					Type t when t == typeof(double) => double.Parse(stringValue),
					Type t when t == typeof(decimal) => decimal.Parse(stringValue),
					Type t when t == typeof(byte) => byte.Parse(stringValue),
					Type t when t == typeof(sbyte) => sbyte.Parse(stringValue),
					_ => throw new NotSupportedException()
				};
				return true;
			}
		}

		/// <summary>
		/// ValueConverter for EntityFramework. Its purpose is to allow the usage of the StrongTypedId in your Entity classes.
		/// Add it to your DbContext class in protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) like this:
		/// configurationBuilder.Properties<UserId>().HaveConversion<UserId.StrongTypedIdValueConverter>();
		/// </summary>
		/*
		public class StrongTypedIdValueConverter : ValueConverter<TStrongTypedId, TPrimitiveId>
		{
			public StrongTypedIdValueConverter()
				: base(id => id.PrimitiveId, primitiveId => Create(primitiveId))
			{
			}
		}*/

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

		private static TStrongTypedId Create(TPrimitiveId value)
		{
			var ctor = GetOrCreateCtor();
			var instance = ctor!.Invoke(value);
			return instance;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "We know the ctor is protected and have control over this")]
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

		public static bool operator ==(StrongTypedId<TStrongTypedId, TPrimitiveId>? a, StrongTypedId<TStrongTypedId, TPrimitiveId>? b)
		{
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