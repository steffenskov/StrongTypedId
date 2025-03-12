# StrongTypedId.NewtonSoft

This package provides support for using StrongTypedId with the NewtonSoft JSON serializer.

# Usage

Add the `StrongTypedNewtonSoftJsonConverter` to your serializer settings, there's no need to decorate your types with an
attribute any more.

```
var settings = new JsonSerializerSettings
{
    Converters = { new StrongTypedNewtonSoftJsonConverter() }
};

var json = JsonConvert.SerializeObject(UserId.New(), settings);
```

OR

```
var serializer = new JsonSerializer()
{
    Converters = { new StrongTypedNewtonSoftJsonConverter() }
};

var json = serializer.Serialize(writer, UserId.New());
```

# Obsolete: Old Usage

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

Notice how you can have both JsonConverters applied simultaneously to support both WebAPI and NewtonSoft at the same
time.

# Documentation

Auto generated documentation via [DocFx](https://github.com/dotnet/docfx) is available
here: https://steffenskov.github.io/StrongTypedId/