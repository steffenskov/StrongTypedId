# StrongTypedId

A super small library for providing strong typed Ids (as opposed to using primitives).

The benefit of this is simple: You don't run the risk of accidentally using the wrong type of id. (e.g. sending a UserId
into a query for products)

This works through the use of an abstract base class (`StrongTypedId<TStrongTypedId, TPrimitiveId>`) which is inherited
to gain the id functionality.

This project is inspired by [Andrew Lock's StronglyTypedId](https://github.com/andrewlock/StronglyTypedId).
However I needed support for .Net 5 and thus this project was born. It has since evolved to .Net 9.

# Installation

I recommend using the NuGet package: [StrongTypedId](https://www.nuget.org/packages/StrongTypedId) however feel free to
clone the source instead if that suits your needs better.

# Usage

Specify your class like this:

```
public partial class UserId: StrongTypedId<UserId, Guid>
{
	public UserId(Guid value) : base(value)
	{
	}
}
```

This specifies that the class `UserId` is in fact a `Guid` and can be used in place of a `Guid`.
And that's basically all there is to it, now you just use `UserId` in place of `Guid` where you're dealing with an User'
s Id.

Note: The class is marked partial because a source generator is using this to apply a `JsonConverter` attribute to it.
If you don't need a `JsonConverter` or you'd rather specify the attributes yourself (such as e.g. on a `file` visible
class), feel free to omit the `partial` keyword.

Furthermore there are a couple of base classes available to you:

- `StrongTypedValue` for anything that's not an id, this supports `string` as a primitive value.
- `StrongTypedId` for anything that IS an id, this only supports `struct` types as primitives (therefore no `strings`).
    - Adds the static `Parse(string)` and `TryParse(string, out TStrongTypedId)` methods.
- `StrongTypedGuid` a further specialization of `StrongTypedId`.
    - Adds the static `New()` method for instantiating new ids with random values as well as the static `Empty`
      property.

Finally you can recognize a `StrongTyped` value by calling the extension method `IsStrongTypedValue()`. All strong typed
values furthermore implements both `IStrongTypedValue<TPrimitiveValue>` as well as `IStrongTypedValue` (The latter being
strictly a marker interface).

# Documentation

Auto generated documentation via [DocFx](https://github.com/dotnet/docfx) is available
here: https://steffenskov.github.io/StrongTypedId/

# Compatibility

## Dapper.DDD.Repository

This can work without any extensions, however it's a bit simpler to use via the
package [StrongTypedId.Dapper.DDD.Repository](https://www.nuget.org/packages/StrongTypedId.Dapper.DDD.Repository/).

## Entity Framework

This is supported through the
package [StrongTypedId.EntityFrameworkCore](https://www.nuget.org/packages/StrongTypedId.EntityFrameworkCore).

## WebAPI

This is supported out-of-the-box.

## MVC

This is supported out-of-the-box.

## NewtonSoft.Json

This is supported through the
package [StrongTypedId.NewtonSoft](https://www.nuget.org/packages/StrongTypedId.NewtonSoft).

## Swagger

This is supported through the package [StrongTypedId.Swagger](https://www.nuget.org/packages/StrongTypedId.Swagger).