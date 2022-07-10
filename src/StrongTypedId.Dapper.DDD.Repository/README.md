# StrongTypedId.Dapper.DDD.Repository

This is a small extension method for making usage of StrongTypedIds easier with the [Dapper.DDD.Repository](https://github.com/steffenskov/Dapper.DDD.Repository).

# Usage

Add type converters like this to your Dapper.DDD.Repository DefaultConfiguration:

```
public class UserId : StrongTypedId<UserId, Guid>
{
	public UserId(Guid primitiveId) : base(primitiveId)
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