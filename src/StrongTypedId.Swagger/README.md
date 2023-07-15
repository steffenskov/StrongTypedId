# StrongTypedId.Swagger

This package provides support for using StrongTypedId with Swagger, to provide proper type mapping for your APIs.

# Usage

Call `MapStrongType` during your Swagger setup like below:

```
builder.Services.AddSwaggerGen(options => {
    options.MapStrongType<UserId, Guid>();
    options.MapStrongType<EmailAddress, string>();
});
```

There's also the option of mapping types without generics:

```
builder.Services.AddSwaggerGen(options => {
    options.MapStrongType(typeof(UserId), typeof(Guid));
});
```

~~~~The latter can be useful, if you're using Reflection to search your AppDomain for all types and map them.