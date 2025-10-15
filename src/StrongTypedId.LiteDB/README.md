# StrongTypedId.MongoDB

This package provides support for using StrongTypedId with LiteDB by adding LiteDB serializers.

# Usage

Simply reference the project and create a custom `BsonMapper` using the `StrongTypedLiteDB.CreateBsonMapper` method.
Feed this `BsonMapper` to your `LiteDatabase` constructor and you're set.

```
// There are multiple ways to get all relevant assemblies, the method takes a params IEnumerable<Assembly>, so feel free to do this however you want
var mapper = StrongTypedLiteDB.CreateBsonMapper(typeof(SomeId).Assembly);

var liteDb = new LiteDatabase("my-db.db", mapper); // this liteDb instance is now compatible with all StrongTyped types in the assemblies above.
```

You can wire up as many assemblies as you want in a single call.

# Documentation

Auto generated documentation via [DocFx](https://github.com/dotnet/docfx) is available
here: https://steffenskov.github.io/StrongTypedId/