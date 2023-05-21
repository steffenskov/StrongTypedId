using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace StrongTypedId.Converters
{
	/// <Summary>
	/// ValueConverter for EntityFramework. Its purpose is to allow the usage of the StrongTypedId in your Entity classes.
	/// Add it to your DbContext class in protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) like this:
	/// configurationBuilder.Properties<UserId>().HaveConversion<StrongTypedIdValueConverter<UserId, Guid>>();
	/// </Summary>
	public class StrongTypedIdValueConverter<TStrongTypedId, TPrimitiveId> : ValueConverter<TStrongTypedId, TPrimitiveId>
		where TStrongTypedId : StrongTypedId<TStrongTypedId, TPrimitiveId>
		where TPrimitiveId : struct, IComparable, IComparable<TPrimitiveId>, IEquatable<TPrimitiveId>, IParsable<TPrimitiveId>
	{
		public StrongTypedIdValueConverter()
			: base(id => id.PrimitiveId, primitiveId => StrongTypedId<TStrongTypedId, TPrimitiveId>.Create(primitiveId))
		{
		}
	}
}