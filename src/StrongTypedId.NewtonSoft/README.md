# StrongTypedId.NewtonSoft

This package provides support for using StrongTypedId with the NewtonSoft JSON serializer.

# Usage

Add a `NewtonSoft.Json.JsonConverter` similarly to the built-in converters:

```
[TypeConverter(typeof(StrongTypedIdTypeConverter<UserId, Guid>))]
[StrongTypedIdJsonConverter<UserId, Guid>]
[Newtonsoft.Json.JsonConverter(typeof(NewtonSoftJsonConverter<UserId, Guid>))]
public class UserId: StrongTypedId<UserId, Guid>
{
	public UserId(Guid value) : base(value)
	{
	}
}
```

Notice how you can have both JsonConverters applied simultaneously to support both WebAPI and NewtonSoft at the same time.

# Documentation
Auto generated documentation via [DocFx](https://github.com/dotnet/docfx) is available here: https://steffenskov.github.io/StrongTypedId/