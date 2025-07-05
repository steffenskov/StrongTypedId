# StrongTypedId.NewtonSoft

This package provides support for using StrongTypedId with the NewtonSoft JSON serializer.

# Usage

First add the `StrongTypedNewtonSoftJsonConverter` to your serializer settings.

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

This will serialize any `StrongTypedId`, `StrongTypedGuid` or `StrongTypedValue` with explicit type information,
allowing deserialization even if your model is abstract.

If you furthermore have types you'd prefer serialized as their underlying value, you can add a
`Newtonsoft.Json.JsonConverter` attribute to the type:

```


[Newtonsoft.Json.JsonConverter(typeof(NewtonSoftJsonConverter<UserId, Guid>))] // This is the converter for NewtonSoft.

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