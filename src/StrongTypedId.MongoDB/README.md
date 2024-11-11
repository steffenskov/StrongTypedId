# StrongTypedId.MongoDB

This package provides support for using StrongTypedId with MongoDB by adding a MongoDB serializer.

# Usage

Simply reference the project and execute `StrongTypedMongo.AddStrongTypedMongoSerializers` with your assemblies
containing StrongTyped types.

```
StrongTypedMongo.AddStrongTypedMongoSerializers(typeof(MyId).Assembly);

// or

StrongTypedMongo.AddStrongTypedMongoSerializers(typeof(MyId).Assembly, typeof(IdInOtherAssembly).Assembly);
```

You can wire up as many assemblies as you want in a single call.