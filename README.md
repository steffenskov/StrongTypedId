# StrongTypedId
A super small library for providing strong typed Ids (as opposed to using primitives).

The benefit of this is simple: You don't run the risk of accidentally using the wrong type of id. (e.g. sending a UserId into a query for products)

This works through the use of an abstract base class (`StrongTypedId<TStrongTypedId, TPrimitiveId>`) which is inherited to gain the id functionality.

This project is inspired by Andrew Lock's StronglyTypedId: https://github.com/andrewlock/StronglyTypedId.
However I needed support for .Net 5 and thus this project was born.

# Installation
I recommend using the NuGet package: https://www.nuget.org/packages/StrongTypedId/ however you can also simply copy the class StrongTypedId into your project, as it's a single class solution.

# Usage

Specify your class like this:
```
[TypeConverter(typeof(UserId.StrongTypedIdTypeConverter))]
[JsonConverter(typeof(UserId.StrongTypedIdJsonConverter))]
public class UserId: StrongTypedId<UserId, Guid>
{
	public UserId(Guid value) : base(value)
	{
	}
}
```

This specifies that the class UserId is in fact a Guid and can be used in place of a Guid.
And that's basically all there is to it, now you just use UserId in place of Guid where you're dealing with an User's Id.

You can omit the `JsonConverter` if you don't use json serialization.



# Compatibility

## WebAPI
This is supported through the use of the `JsonConverter` and `TypeConverter`.

## NewtonSoft.Json
You'll need a NewtonSoft based `JsonConverter` for this. I've added such a class in the source code, but commented it out to prevent needless references to NewtonSoft for those not using that.
I'm still working on how to offer both options via NuGet, so for now your best bet is to copy the source class into your project and uncomment the `StrongTypedIdNewtonSoftJsonConverter` class within.

After doing that you just add the `JsonConverter` in the same fashion as the `System.Text.Json` based one:
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
You'll need to add a conversion for EF to work properly. I've added a ValueConverter class to the source code, but commented it out to prevent needless references to EF for those not using that.
I'm still contemplating how to offer both options via NuGet, but for now your best bet is to copy the source class into your project and uncomment the `StrongTypedIdValueConverter` class within.

After doing that you need to add the converter to EF in your `DbContext` class like this:

```
protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
	configurationBuilder.Properties<UserId>().HaveConversion<UserId.StrongTypedIdValueConverter>();
}
```
