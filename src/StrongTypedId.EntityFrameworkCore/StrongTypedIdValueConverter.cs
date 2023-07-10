using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace StrongTypedId.Converters
{
	/// <Summary>
	/// ValueConverter for EntityFramework. Its purpose is to allow the usage of the StrongTypedId in your Entity classes.
	/// Add it to your DbContext class in protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) like this:
	/// configurationBuilder.Properties&lt;UserId&gt;().HaveConversion&lt;StrongTypedIdValueConverter&lt;UserId, Guid&gt;&gt;();
	/// </Summary>
	public class StrongTypedIdValueConverter<TStrongTypedId, TPrimitiveId> : StrongTypedValueValueConverter<TStrongTypedId, TPrimitiveId>
		where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>, IParsable<TPrimitiveId>
	{
	}
	
	/// <Summary>
	/// ValueConverter for EntityFramework. Its purpose is to allow the usage of the StrongTypedId in your Entity classes.
	/// Add it to your DbContext class in protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) like this:
	/// configurationBuilder.Properties&lt;UserId&gt;().HaveConversion&lt;StrongTypedIdValueConverter&lt;UserId, Guid&gt;&gt;();
	/// </Summary>
	public class StrongTypedValueValueConverter<TStrongTypedValue, TPrimitiveValue> : ValueConverter<TStrongTypedValue, TPrimitiveValue>
		where TStrongTypedValue : StrongTypedValue<TStrongTypedValue, TPrimitiveValue>
		where TPrimitiveValue : IComparable, IComparable<TPrimitiveValue>, IEquatable<TPrimitiveValue>, IParsable<TPrimitiveValue>
	{
		public StrongTypedValueValueConverter()
			: base(id => id.PrimitiveId, primitiveId => StrongTypedValue<TStrongTypedValue, TPrimitiveValue>.Create(primitiveId))
		{
		}
	}
}