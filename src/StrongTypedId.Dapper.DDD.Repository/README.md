# StrongTypedId.Dapper.DDD.Repository

This package simplifies the usage of StrongTypedIds with [Dapper.DDD.Repository](https://github.com/steffenskov/Dapper.DDD.Repository).

# Usage

Add type converters like this to your Dapper.DDD.Repository DefaultConfiguration:

```
public class UserId : StrongTypedId<UserId, Guid>
{
	public UserId(Guid value) : base(value)
	{
	}
}
```
```
	...
	services.ConfigureDapperRepositoryDefaults(options =>
	{
		options.AddTypeConverter(UserId.New().GetTypeConverter());
	});
	...
```