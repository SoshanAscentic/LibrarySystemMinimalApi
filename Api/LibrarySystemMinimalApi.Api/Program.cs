using LibrarySystemMinimalApi.Api.Endpoints;
using LibrarySystemMinimalApi.Application.Extensions;
using LibrarySystemMinimalApi.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library System API",
        Version = "v1",
        Description = "A comprehensive library management system API built with Minimal APIs",
        Contact = new OpenApiContact
        {
            Name = "Library System Team",
            Email = "admin@librarysystem.com"
        }
    });

    options.EnableAnnotations();
    options.DescribeAllParametersInCamelCase();

    options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

builder.Services.AddDataServices();
builder.Services.AddApplicationServices();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library System API v1");
        c.RoutePrefix = string.Empty; // Serve at root

        // Enhanced UI features
        c.DefaultModelsExpandDepth(1);
        c.DefaultModelExpandDepth(1);
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
        c.ShowExtensions();
        c.EnableValidator();
    });
}

app.UseHttpsRedirection();

app.MapAuthEndpoints();
app.MapBookEndpoints();
app.MapMemberEndpoints();
app.MapBorrowingEndpoints();

app.MapGet("/", () => Results.Ok(new
{
    message = "Welcome to Library System API (Minimal APIs)",
    version = "1.0.0",
    documentation = "/swagger",
    apiSpec = "/swagger/v1/swagger.json",
    endpoints = new[] {
        "/api/auth/login",
        "/api/auth/signup",
        "/api/books",
        "/api/members",
        "/api/borrowing/borrow",
        "/api/borrowing/return"
    }
}))
.WithName("GetApiInfo")
.WithTags("General")
.WithSummary("Get API information")
.Produces<object>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    timestamp = DateTime.UtcNow,
    version = "1.0.0",
    swagger = "9.0.1"
}))
.WithName("HealthCheck")
.WithTags("General")
.WithSummary("API health check")
.Produces<object>(StatusCodes.Status200OK)
.WithOpenApi();

app.Run();