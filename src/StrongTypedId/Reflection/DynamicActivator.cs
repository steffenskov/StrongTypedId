using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using StrongTypedId.Collections;

namespace StrongTypedId.Reflection;

static internal class DynamicActivator
{
	private static readonly LockedConcurrentDictionary<Type, Func<object, object>> _constructors = new();

	public static object Create(Type type, object primitiveValue)
	{
		var ctor = GetOrCreateCtor(type, primitiveValue.GetType());
		var instance = ctor.Invoke(primitiveValue);
		return instance;
	}

	[SuppressMessage("Major Code Smell",
		"S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
		Justification = "We know the ctor is protected and have control over this")]
	private static Func<object, object> GetOrCreateCtor(Type idType, Type primitiveType)
	{
		return _constructors.GetOrAdd(idType, type =>
		{
			var ctor = type.GetConstructor(
				BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
				new[] { primitiveType }, null);

			if (ctor is null)
			{
				throw new InvalidOperationException($"No constructor found for type {type.Name} with one argument of type {primitiveType.Name}.");
			}

			return Helper.CreateDelegate(ctor);
		});
	}
}

internal class DynamicActivator<TSelf, TPrimitiveValue>
	where TSelf : StrongTypedValue<TSelf, TPrimitiveValue>
	where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>
{
	private readonly LockedConcurrentDictionary<Type, Func<TPrimitiveValue, TSelf>> _constructors = new();

	public TSelf Create(TPrimitiveValue value)
	{
		var ctor = GetOrCreateCtor();
		var instance = ctor.Invoke(value);
		return instance;
	}

	[SuppressMessage("Major Code Smell",
		"S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
		Justification = "We know the ctor is protected and have control over this")]
	private Func<TPrimitiveValue, TSelf> GetOrCreateCtor()
	{
		var idType = typeof(TSelf);
		return _constructors.GetOrAdd(idType, type =>
		{
			var ctor = type.GetConstructor(
				BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
				new[] { typeof(TPrimitiveValue) }, null);

			if (ctor is null)
			{
				throw new InvalidOperationException($"No constructor found for type {type.Name} with one argument of type {typeof(TPrimitiveValue).Name}.");
			}

			return Helper.CreateDelegate<TPrimitiveValue, TSelf>(ctor);
		});
	}
}

static file class Helper
{
	public static Func<TInput, TOutput> CreateDelegate<TInput, TOutput>(ConstructorInfo constructor)
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

		return (Func<TInput, TOutput>)method.CreateDelegate(typeof(Func<,>).MakeGenericType(typeof(TInput), typeof(TOutput)));
	}

	public static Func<object, object> CreateDelegate(ConstructorInfo constructor)
	{
		var constructorParam = constructor.GetParameters();
		var inputType = constructorParam[0].ParameterType;

		// Create the dynamic method
		var method =
			new DynamicMethod($"{constructor.DeclaringType!.Name}__{Guid.NewGuid().ToString().Replace("-", "")}",
				constructor.DeclaringType,
				[typeof(object)],
				true
			);

		// Create the il
		var gen = method.GetILGenerator();
		gen.Emit(OpCodes.Ldarg_0);
		if (inputType.IsValueType)
		{
			gen.Emit(OpCodes.Unbox_Any, inputType);
		}
		else
		{
			gen.Emit(OpCodes.Castclass, inputType);
		}

		gen.Emit(OpCodes.Newobj, constructor);
		gen.Emit(OpCodes.Ret);


		return (Func<object, object>)method.CreateDelegate(typeof(Func<object, object>));
	}
}