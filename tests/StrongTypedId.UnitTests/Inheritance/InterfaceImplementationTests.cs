namespace StrongTypedId.UnitTests.Inheritance;

public class InterfaceImplementationTests
{
	#region StrongTypedGuid

	[Fact]
	public void StrongTypedGuid_IStrongTypedGuid_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedGuidId).IsAssignableTo(typeof(IStrongTypedGuid)));
	}

	[Fact]
	public void StrongTypedGuid_IStrongTypedId_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedGuidId).IsAssignableTo(typeof(IStrongTypedId<Guid>)));
	}

	[Fact]
	public void StrongTypedGuid_IStrongTypedValue_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedGuidId).IsAssignableTo(typeof(IStrongTypedValue<Guid>)));
	}

	[Fact]
	public void StrongTypedGuid_IStrongTypedValueNonGeneric_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedGuidId).IsAssignableTo(typeof(IStrongTypedValue)));
	}

	#endregion

	#region StrongTypedId

	[Fact]
	public void StrongTypedId_IStrongTypedGuid_DoesNotImplement()
	{
		// Assert
		Assert.False(typeof(AttributedIntId).IsAssignableTo(typeof(IStrongTypedGuid)));
	}

	[Fact]
	public void StrongTypedId_IStrongTypedId_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedIntId).IsAssignableTo(typeof(IStrongTypedId<int>)));
	}

	[Fact]
	public void StrongTypedId_IStrongTypedValue_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedIntId).IsAssignableTo(typeof(IStrongTypedValue<int>)));
	}

	[Fact]
	public void StrongTypedId_IStrongTypedValueNonGeneric_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedIntId).IsAssignableTo(typeof(IStrongTypedValue)));
	}

	#endregion

	#region StrongTypedValue

	[Fact]
	public void StrongTypedValue_IStrongTypedGuid_DoesNotImplement()
	{
		// Assert
		Assert.False(typeof(AttributedEmailAddress).IsAssignableTo(typeof(IStrongTypedGuid)));
	}

	[Fact]
	public void StrongTypedValue_IStrongTypedId_DoesNotImplement()
	{
		// Assert
		Assert.False(typeof(AttributedEmailAddress).IsAssignableTo(typeof(IStrongTypedId<string>)));
	}

	[Fact]
	public void StrongTypedValue_IStrongTypedValue_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedEmailAddress).IsAssignableTo(typeof(IStrongTypedValue<string>)));
	}

	[Fact]
	public void StrongTypedValue_IStrongTypedValueNonGeneric_Implements()
	{
		// Assert
		Assert.True(typeof(AttributedEmailAddress).IsAssignableTo(typeof(IStrongTypedValue)));
	}

	#endregion
}