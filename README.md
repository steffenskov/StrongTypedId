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

You can omit the JsonConverter if you don't use json serialization.



# Compatibility

## WebAPI
This is supported through the use of the JsonConverter.

## Entity Framework
You'll need to add a conversion for EF to work properly, luckily this is easily done in the `OnModelCreating` method on your `DbContext`.

Assume you have an User class which uses the UserId:
```
public class User
{
	public UserId Id { get; set; }
	public string Username { get;set; }
	// And so forth
}
```

You'll then add the conversion for the UserId in EF like this:

```
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
	modelBuilder.Entity<User>(entity =>
	{
		entity.HasKey(e => e.Id);

		entity.Property(e => e.Id)
				.HasConversion(e => e.PrimitiveId, primitiveId => new UserId(primitiveId));
	});
}
```