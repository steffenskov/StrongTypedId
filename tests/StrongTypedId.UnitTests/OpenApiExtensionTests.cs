using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using StrongTypedId.OpenApi;

namespace StrongTypedId.UnitTests;

public class OpenApiStrongTypeExtensionTests
{
	#region Host helper

	private static async Task<Dictionary<string, JsonElement>> GetSchemasAsync(Action<OpenApiOptions> configure)
	{
		var builder = WebApplication.CreateBuilder();
		builder.WebHost.UseTestServer();
		builder.Services.AddOpenApi(configure);

		await using var app = builder.Build();
		app.MapOpenApi();
		app.MapPost("/orders", (OrderDto dto) => Results.Ok(dto));
		app.MapGet("/orders/{id}", (OrderId id) => Results.Ok(id));
		app.MapGet("/customers/{id}", (CustomerId id) => Results.Ok(id));
		await app.StartAsync();

		var json = await app.GetTestClient().GetStringAsync("/openapi/v1.json");
		using var document = JsonDocument.Parse(json);

		var result = new Dictionary<string, JsonElement>();
		if (document.RootElement.TryGetProperty("components", out var components) &&
		    components.TryGetProperty("schemas", out var schemas))
		{
			foreach (var property in schemas.EnumerateObject())
			{
				result[property.Name] = property.Value.Clone();
			}
		}

		return result;
	}

	#endregion

	#region Fixtures

	// NOTE: adjust the constructors to match StrongTypedValue's actual base constructor.
	public class OrderId : StrongTypedId<OrderId, Guid>
	{
		public OrderId(Guid value) : base(value)
		{
		}
	}

	public class CustomerId : StrongTypedId<CustomerId, int>
	{
		public CustomerId(int value) : base(value)
		{
		}
	}

	public class NotAStrongType
	{
		public string Name { get; set; } = "";
	}

	// Gives the built-in generator at least one component schema, so doc.Components.Schemas is non-null.
	public record OrderDto(string Name, OrderId Id, CustomerId CustomerId);

	#endregion

	#region Fluent API / registration

	[Fact]
	public void MapStrongType_Generic_ReturnsSameOptionsInstance()
	{
		var options = new OpenApiOptions();

		var result = options.MapStrongType<OrderId, Guid>();

		Assert.Same(options, result);
	}

	[Fact]
	public void MapStrongType_NonGeneric_ReturnsSameOptionsInstance()
	{
		var options = new OpenApiOptions();

		var result = options.MapStrongType(typeof(OrderId), typeof(Guid));

		Assert.Same(options, result);
	}

	[Fact]
	public void MapStrongTypesFromAssemblyContaining_Generic_ReturnsSameOptionsInstance()
	{
		var options = new OpenApiOptions();

		Assert.Same(options, options.MapStrongTypesFromAssemblyContaining<OrderId>());
	}

	[Fact]
	public void MapStrongTypesFromAssemblyContaining_Type_ReturnsSameOptionsInstance()
	{
		var options = new OpenApiOptions();

		Assert.Same(options, options.MapStrongTypesFromAssemblyContaining(typeof(OrderId)));
	}

	[Fact]
	public void MapStrongTypesFromAssemblies_EmptyCollection_ReturnsSameOptionsInstance()
	{
		var options = new OpenApiOptions();

		Assert.Same(options, options.MapStrongTypesFromAssemblies([]));
	}

	[Fact]
	public void MapStrongTypesFromAssemblies_NullCollection_Throws()
	{
		var options = new OpenApiOptions();

		Assert.Throws<ArgumentNullException>(() => options.MapStrongTypesFromAssemblies(null!));
	}

	[Fact]
	public void MapStrongTypesFromAssembly_AssemblyWithoutStrongTypes_DoesNotThrow()
	{
		var options = new OpenApiOptions();

		var ex = Record.Exception(() => options.MapStrongTypesFromAssembly(typeof(OpenApiOptions).Assembly));

		Assert.Null(ex);
	}

	#endregion

	#region End-to-end via generated document

	[Fact]
	public async Task MapStrongType_Generic_ReplacesSchemaInDocument()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongType<OrderId, Guid>());

		var schema = Assert.Contains(OpenApiStrongTypeExtension.GetSchemaId(typeof(OrderId)), schemas);
		Assert.Equal("string", schema.GetProperty("type").GetString());
		Assert.Equal("uuid", schema.GetProperty("format").GetString());
		Assert.False(schema.TryGetProperty("properties", out _));
	}

	[Fact]
	public async Task MapStrongType_NonGeneric_ReplacesSchemaInDocument()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongType(typeof(CustomerId), typeof(int)));

		var schema = Assert.Contains(OpenApiStrongTypeExtension.GetSchemaId(typeof(CustomerId)), schemas);
		var primaryType = schema.GetProperty("type")[0];
		Assert.Equal("integer", primaryType.GetString());
		Assert.Equal("int32", schema.GetProperty("format").GetString());
	}

	[Fact]
	public async Task MapStrongType_MultipleTypes_AllAreReplaced()
	{
		var schemas = await GetSchemasAsync(o => o
			.MapStrongType<OrderId, Guid>()
			.MapStrongType<CustomerId, int>());

		Assert.Equal("uuid", schemas[OpenApiStrongTypeExtension.GetSchemaId(typeof(OrderId))].GetProperty("format").GetString());
		Assert.Equal("int32", schemas[OpenApiStrongTypeExtension.GetSchemaId(typeof(CustomerId))].GetProperty("format").GetString());
	}

	[Fact]
	public async Task MapStrongType_DoesNotTouchUnrelatedSchemas()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongType<OrderId, Guid>());

		var dto = Assert.Contains(nameof(OrderDto), schemas);
		Assert.Equal("object", dto.GetProperty("type").GetString());
		Assert.True(dto.TryGetProperty("properties", out _));
	}

	[Fact]
	public async Task MapStrongTypesFromAssembly_MapsAllStrongTypesInAssembly()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongTypesFromAssembly(typeof(OrderId).Assembly));

		Assert.Equal("uuid", schemas[OpenApiStrongTypeExtension.GetSchemaId(typeof(OrderId))].GetProperty("format").GetString());
		Assert.Equal("int32", schemas[OpenApiStrongTypeExtension.GetSchemaId(typeof(CustomerId))].GetProperty("format").GetString());
		Assert.DoesNotContain(OpenApiStrongTypeExtension.GetSchemaId(typeof(NotAStrongType)), schemas);
	}

	[Fact]
	public async Task MapStrongTypesFromAssemblyContaining_Generic_MapsStrongTypes()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongTypesFromAssemblyContaining<OrderId>());

		Assert.Contains(OpenApiStrongTypeExtension.GetSchemaId(typeof(OrderId)), schemas);
		Assert.Contains(OpenApiStrongTypeExtension.GetSchemaId(typeof(CustomerId)), schemas);
	}

	[Fact]
	public async Task MapStrongTypesFromAssemblyContaining_Type_MapsStrongTypes()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongTypesFromAssemblyContaining(typeof(OrderId)));

		Assert.Contains(OpenApiStrongTypeExtension.GetSchemaId(typeof(OrderId)), schemas);
		Assert.Contains(OpenApiStrongTypeExtension.GetSchemaId(typeof(CustomerId)), schemas);
	}

	[Fact]
	public async Task MapStrongTypesFromAssemblies_MapsStrongTypesFromEveryAssembly()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongTypesFromAssemblies(
			[typeof(OpenApiOptions).Assembly, typeof(OrderId).Assembly]));

		Assert.Contains(OpenApiStrongTypeExtension.GetSchemaId(typeof(OrderId)), schemas);
		Assert.Contains(OpenApiStrongTypeExtension.GetSchemaId(typeof(CustomerId)), schemas);
	}

	[Fact]
	public async Task NoMapping_DocumentContainsNoStrongTypeSchemas()
	{
		var schemas = await GetSchemasAsync(_ => { });

		Assert.DoesNotContain(OpenApiStrongTypeExtension.GetSchemaId(typeof(OrderId)), schemas);
		Assert.DoesNotContain(OpenApiStrongTypeExtension.GetSchemaId(typeof(CustomerId)), schemas);
	}

	#endregion
}

file sealed class FakeType
{
}