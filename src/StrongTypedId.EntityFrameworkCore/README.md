# StrongTypedId.EntityFrameworkCore

This package provides support for using StrongTypedId with EntityFrameworkCore.

# Usage

Add the converter to EF in your `DbContext` class like this:

```
protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
	configurationBuilder
		.Properties<UserId>()
		.HaveConversion<StrongTypedIdValueConverter<UserId, Guid>>();
}
```

And that's all there is to it, now any of your entities can use the `UserId` type instead of `Guid`.