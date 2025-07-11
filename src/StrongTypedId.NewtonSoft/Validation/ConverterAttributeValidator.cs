using System.Reflection;
using Newtonsoft.Json;
using StrongTypedId.NewtonSoft.Exceptions;

namespace StrongTypedId.NewtonSoft.Validation;

internal class ConverterAttributeValidator
{
	public void ValidateType(Type type)
	{
		var attributes = type.GetCustomAttributes();

		// FirstOrDefault because the attribute doesn't allow multiples, so we don't have to check for that
		var converterAttribute = attributes.FirstOrDefault(attr => attr is JsonConverterAttribute) as JsonConverterAttribute;

		if (converterAttribute is null)
		{
			return;
		}

		var (tSelf, tPrimitive) = type.GetStrongTypedValueArguments();

		var converterType = converterAttribute.ConverterType;

		var genericConverterArguments = converterType.GetGenericArguments();

		if (genericConverterArguments[0] != tSelf || genericConverterArguments[1] != tPrimitive)
		{
			throw new AttributeMismatchException(tSelf);
		}
	}
}