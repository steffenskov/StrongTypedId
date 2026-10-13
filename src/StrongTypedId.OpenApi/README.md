# Want to see what's changed?

[Changelog](https://github.com/steffenskov/StrongTypedId/blob/main/CHANGELOG.md)

# StrongTypedId.OpenApi

This package provides support for using StrongTypedId with OpenApi, to provide proper type mapping for your APIs.

# Usage

Preferably map all types at once by calling `MapStrongTypesFromAssemblies` or `MapStrongTypesFromAssemblyContaining` during your OpenApi setup like below:

```csharp
builder.Services.AddOpenApi(options =>
{
    options.MapStrongTypesFromAssemblyContaining<UserId>(); // Will scan all types within the same assembly as UserId
	options.MapStrongTypesFromAssemblies(myAssemblies); // Can scan multiple assemblies at once
});
```

You can also map single types if need be:

```csharp
builder.Services.AddOpenApi(options =>
{
    options.MapStrongType<UserId, Guid>(); // Map a single type using generics
	options.MapStrongType(typeof(CustomerId), typeof(Guid)); // Map a single type manually
});
```

# Documentation

Auto generated documentation via [DocFx](https://github.com/dotnet/docfx) is available here: https://steffenskov.github.io/StrongTypedId/