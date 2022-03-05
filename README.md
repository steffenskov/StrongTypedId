# StrongTypedId
A super small library for providing strong typed Ids (as opposed to using primitives).

The benefit of this is simple: You don't run the risk of accidentally using the wrong type of id. (e.g. sending a UserId into a query for products)

This works through the use of an abstract base class (`StrongTypedId<TConcreteClass, TValue>`) which is inherited to gain the id functionality.

This project is inspired by Andrew Lock's StronglyTypedId: https://github.com/andrewlock/StronglyTypedId.
However I needed support for .Net 5 and thus this project was born.

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