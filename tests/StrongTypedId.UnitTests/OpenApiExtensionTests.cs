using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
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
		app.MapPost("/orders", (OrderDto dto) => Results.Ok());
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
	public class OrderId : StrongTypedValue<OrderId, Guid>
	{
		public OrderId(Guid value) : base(value)
		{
		}
	}

	public class CustomerId : StrongTypedValue<CustomerId, int>
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
	public record OrderDto(string Name);

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

		var schema = Assert.Contains(nameof(OrderId), schemas);
		Assert.Equal("string", schema.GetProperty("type").GetString());
		Assert.Equal("uuid", schema.GetProperty("format").GetString());
		Assert.False(schema.TryGetProperty("properties", out _));
	}

	[Fact]
	public async Task MapStrongType_NonGeneric_ReplacesSchemaInDocument()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongType(typeof(CustomerId), typeof(int)));

		var schema = Assert.Contains(nameof(CustomerId), schemas);
		Assert.Equal("integer", schema.GetProperty("type").GetString());
		Assert.Equal("int32", schema.GetProperty("format").GetString());
	}

	[Fact]
	public async Task MapStrongType_MultipleTypes_AllAreReplaced()
	{
		var schemas = await GetSchemasAsync(o => o
			.MapStrongType<OrderId, Guid>()
			.MapStrongType<CustomerId, int>());

		Assert.Equal("uuid", schemas[nameof(OrderId)].GetProperty("format").GetString());
		Assert.Equal("int32", schemas[nameof(CustomerId)].GetProperty("format").GetString());
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

		Assert.Equal("uuid", schemas[nameof(OrderId)].GetProperty("format").GetString());
		Assert.Equal("int32", schemas[nameof(CustomerId)].GetProperty("format").GetString());
		Assert.DoesNotContain(nameof(NotAStrongType), schemas);
	}

	[Fact]
	public async Task MapStrongTypesFromAssemblyContaining_Generic_MapsStrongTypes()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongTypesFromAssemblyContaining<OrderId>());

		Assert.Contains(nameof(OrderId), schemas);
		Assert.Contains(nameof(CustomerId), schemas);
	}

	[Fact]
	public async Task MapStrongTypesFromAssemblyContaining_Type_MapsStrongTypes()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongTypesFromAssemblyContaining(typeof(OrderId)));

		Assert.Contains(nameof(OrderId), schemas);
		Assert.Contains(nameof(CustomerId), schemas);
	}

	[Fact]
	public async Task MapStrongTypesFromAssemblies_MapsStrongTypesFromEveryAssembly()
	{
		var schemas = await GetSchemasAsync(o => o.MapStrongTypesFromAssemblies(
			[typeof(OpenApiOptions).Assembly, typeof(OrderId).Assembly]));

		Assert.Contains(nameof(OrderId), schemas);
		Assert.Contains(nameof(CustomerId), schemas);
	}

	[Fact]
	public async Task NoMapping_DocumentContainsNoStrongTypeSchemas()
	{
		var schemas = await GetSchemasAsync(_ => { });

		Assert.DoesNotContain(nameof(OrderId), schemas);
		Assert.DoesNotContain(nameof(CustomerId), schemas);
	}

	#endregion

	#region Private helpers (via reflection)

	public static TheoryData<Type, JsonSchemaType, string> PrimitiveMappings => new()
	{
		{ typeof(bool), JsonSchemaType.Boolean, "" },
		{ typeof(char), JsonSchemaType.String, "" },
		{ typeof(string), JsonSchemaType.String, "" },
		{ typeof(Guid), JsonSchemaType.String, "uuid" },
		{ typeof(short), JsonSchemaType.Integer, "int16" },
		{ typeof(int), JsonSchemaType.Integer, "int32" },
		{ typeof(long), JsonSchemaType.Integer, "int64" },
		{ typeof(ushort), JsonSchemaType.Integer, "uint16" },
		{ typeof(uint), JsonSchemaType.Integer, "uint32" },
		{ typeof(ulong), JsonSchemaType.Integer, "uint64" },
		{ typeof(float), JsonSchemaType.Number, "float" },
		{ typeof(double), JsonSchemaType.Number, "double" },
		{ typeof(decimal), JsonSchemaType.Number, "" },
		{ typeof(byte), JsonSchemaType.Integer, "uint8" },
		{ typeof(sbyte), JsonSchemaType.Integer, "int8" },
		// Anything unmapped falls back to object
		{ typeof(DateTime), JsonSchemaType.Object, "" },
		{ typeof(DateTimeOffset), JsonSchemaType.Object, "" }
	};

	[Theory]
	[MemberData(nameof(PrimitiveMappings))]
	public void GetOpenApiSchema_MapsPrimitiveToExpectedTypeAndFormat(Type primitive, JsonSchemaType expectedType, string expectedFormat)
	{
		var method = typeof(OpenApiStrongTypeExtension).GetMethod("GetOpenApiSchema", BindingFlags.NonPublic | BindingFlags.Static)!;

		var (type, format) = ((JsonSchemaType, string))method.Invoke(null, [primitive])!;

		Assert.Equal(expectedType, type);
		Assert.Equal(expectedFormat, format);
	}

	[Fact]
	public async Task ReplaceSchema_ExistingComponent_IsOverwritten()
	{
		var doc = new OpenApiDocument
		{
			Components = new OpenApiComponents
			{
				Schemas = new Dictionary<string, IOpenApiSchema>
				{
					[nameof(OrderId)] = new OpenApiSchema
					{
						Type = JsonSchemaType.Object,
						Properties = new Dictionary<string, IOpenApiSchema>
						{
							["value"] = new OpenApiSchema { Type = JsonSchemaType.String }
						}
					}
				}
			}
		};

		await InvokeReplaceSchema(doc, typeof(OrderId), typeof(Guid));

		var schema = doc.Components.Schemas[nameof(OrderId)];
		Assert.Equal(JsonSchemaType.String, schema.Type);
		Assert.Equal("uuid", schema.Format);
		Assert.Null(schema.Properties);
	}

	[Fact]
	public async Task ReplaceSchema_MissingComponent_IsAdded()
	{
		var doc = new OpenApiDocument
		{
			Components = new OpenApiComponents { Schemas = new Dictionary<string, IOpenApiSchema>() }
		};

		await InvokeReplaceSchema(doc, typeof(CustomerId), typeof(int));

		var schema = doc.Components.Schemas[nameof(CustomerId)];
		Assert.Equal(JsonSchemaType.Integer, schema.Type);
		Assert.Equal("int32", schema.Format);
	}

	[Fact]
	public async Task ReplaceSchema_NullSchemas_CompletesWithoutThrowing()
	{
		var doc = new OpenApiDocument { Components = new OpenApiComponents { Schemas = null } };

		await InvokeReplaceSchema(doc, typeof(OrderId), typeof(Guid));

		Assert.Null(doc.Components.Schemas);
	}

	[Fact]
	public async Task ReplaceSchema_NullComponents_CompletesWithoutThrowing()
	{
		var doc = new OpenApiDocument { Components = null };

		await InvokeReplaceSchema(doc, typeof(OrderId), typeof(Guid));

		Assert.Null(doc.Components);
	}

	private static Task InvokeReplaceSchema(OpenApiDocument doc, Type strongType, Type primitiveType)
	{
		var method = typeof(OpenApiStrongTypeExtension).GetMethod("ReplaceSchema", BindingFlags.NonPublic | BindingFlags.Static)!;
		return (Task)method.Invoke(null, [doc, strongType, primitiveType])!;
	}

	#endregion
}