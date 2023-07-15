# StrongTypedId.Swagger

This package provides support for using StrongTypedId with Swagger, to provide proper type mapping for your APIs.
~~~~
# Usage

Call `MapStrongType` during your Swagger setup like below:

```
builder.Services.AddSwaggerGen(options => {
    options.MapStrongType<UserId, Guid>();
    options.MapStrongType<EmailAddress, string>();
    ...
});
```