namespace StrongTypedId.NewtonSoft.Exceptions;

public class AttributeMismatchException : Exception
{
	public AttributeMismatchException(Type tSelf) : base($"The type {tSelf.Name} is decorated with an attribute for another type")
	{
	}
}