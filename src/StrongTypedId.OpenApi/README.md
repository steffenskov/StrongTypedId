# Want to see what's changed?

[Changelog](https://github.com/steffenskov/StrongTypedId/blob/main/CHANGELOG.md)

# StrongTypedId.OpenApi

This package provides support for using StrongTypedId with OpenApi, to provide proper type mapping for your APIs.

# Usage

Call `MapStrongType` during your OpenApi setup like below:

```
builder.Services.AddOpenApi(options =>
{
	options.MapStrongType<UserId, Guid>();
    options.MapStrongType<EmailAddress, string>();
});
```

There's also the option of mapping types without generics:

```
builder.Services.AddOpenApi(options => {
    options.MapStrongType(typeof(UserId), typeof(Guid));
});
```

The latter can be useful, if you're using Reflection to search your AppDomain for all types and map them.~~~~

# Documentation

Auto generated documentation via [DocFx](https://github.com/dotnet/docfx) is available here: https://steffenskov.github.io/StrongTypedId/