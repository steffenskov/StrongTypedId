# StrongTypedId
A super small library for providing strong typed Ids (as opposed to using primitives).

The benefit of this is simple: You don't run the risk of accidentally using the wrong type of id. (e.g. sending a UserId into a query for products)

This works through the use of an abstract base class (`StrongTypedId<TStrongTypedId, TPrimitiveId>`) which is inherited to gain the id functionality.

This project is inspired by [Andrew Lock's StronglyTypedId](https://github.com/andrewlock/StronglyTypedId).
However I needed support for .Net 5 and thus this project was born.

# Installation
I recommend using the NuGet package: [StrongTypedId](https://www.nuget.org/packages/StrongTypedId) however feel free to clone the source instead if that suits your needs better.

# Usage

Specify your class like this:
```
[TypeConverter(typeof(StrongTypedIdTypeConverter<UserId, Guid>))]
[JsonConverter(typeof(SystemTextJsonConverter<UserId, Guid>))]
public class UserId: StrongTypedId<UserId, Guid>
{
	public UserId(Guid primitiveId) : base(primitiveId)
	{
	}
}
```

This specifies that the class UserId is in fact a Guid and can be used in place of a Guid.
And that's basically all there is to it, now you just use UserId in place of Guid where you're dealing with an User's Id.

You can omit the `JsonConverter` if you don't use json serialization as well as the `TypeConverter` if you're not using WebAPI or MVC.


# Compatibility

## Dapper.DDD.Repository
This can work without any extensions, however it's a bit simpler to use via the package [StrongTypedId.Dapper.DDD.Repository](https://www.nuget.org/packages/StrongTypedId.Dapper.DDD.Repository/).

## Entity Framework
This is supported through the package [StrongTypedId.EntityFrameworkCore](https://www.nuget.org/packages/StrongTypedId.EntityFrameworkCore).

## WebAPI
This is supported through the use of the built-in `JsonConverter` and `TypeConverter`.

## MVC
This is supported through the use of the built-in `TypeConverter`.

## NewtonSoft.Json
This is supported through the package [StrongTypedId.NewtonSoft](https://www.nuget.org/packages/StrongTypedId.NewtonSoft).