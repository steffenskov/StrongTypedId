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
[JsonConverter(typeof(SystemTextJsonConverter<UserID, Guid>))]
public class UserId: StrongTypedId<UserId, Guid>
{
	public UserId(Guid value) : base(value)
	{
	}
}
```

This specifies that the class UserId is in fact a Guid and can be used in place of a Guid.
And that's basically all there is to it, now you just use UserId in place of Guid where you're dealing with an User's Id.

You can omit the `JsonConverter` if you don't use json serialization as well as the `TypeConverter` if you're not using WebAPI or MVC.


# Compatibility

## MVC
This is supported through the use of the `TypeConverter`.

## WebAPI
This is supported through the use of the `JsonConverter` and `TypeConverter`.

## NewtonSoft.Json
You'll need a NewtonSoft based `JsonConverter` for this. Add the Nuget package [StrongTypedId.NewtonSoft](https://www.nuget.org/packages/StrongTypedId.NewtonSoft) which includes the `NewtonSoftJsonConverter` class for this purpose.
The reason it's split into another package is to avoid NewtonSoft dependencies in the core package, for those who don't use NewtonSoft.
After adding the package, just add converter similarly to the built-in converters:
```
[TypeConverter(typeof(UserId.StrongTypedIdTypeConverter))]
[JsonConverter(typeof(UserId.StrongTypedIdJsonConverter))]
[Newtonsoft.Json.JsonConverter(typeof(UserId.StrongTypedIdNewtonSoftJsonConverter))]
public class UserId: StrongTypedId<UserId, Guid>
{
	public UserId(Guid value) : base(value)
	{
	}
}
```
Notice how you can have both JsonConverters applied simultaneously to support both WebAPI and NewtonSoft at the same time.

## Entity Framework
You'll need an Entity Framework based `ValueConverter` for this. Add the Nuget package [StrongTypedId.EntityFrameworkCore](https://www.nuget.org/packages/StrongTypedId.EntityFrameworkCore) which includes the `StrongTypedIdValueConverter` class for this purpose.

After doing that you need to add the converter to EF in your `DbContext` class like this:

```
protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
	configurationBuilder
		.Properties<UserId>()
		.HaveConversion<StrongTypedIdValueConverter<UserId, Guid>>();
}
```

And that's all there is to it, now any of your entities can use the `UserId` in place of guids.
