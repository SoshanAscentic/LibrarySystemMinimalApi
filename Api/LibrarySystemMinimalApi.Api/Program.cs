using LibrarySystemMinimalApi.Api.Endpoints;
using LibrarySystemMinimalApi.Application.Extensions;
using LibrarySystemMinimalApi.Data.Context;
using LibrarySystemMinimalApi.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;

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

//Reigster Data and Application Services
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();


//Ensure Database is created and migrated
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An erorr occured while migrating the database");
    }

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
        message = "Welcome to Library System API (Minimal APIs with Entity Framework Core)",
        version = "1.0.0",
        documentation = "/swagger",
        apiSpec = "/swagger/v1/swagger.json",
        database = "SQL Server with Entity Framework Core",
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
        database = "SQL Server",
        framework = "Entity Framework Core"
    }))
    .WithName("HealthCheck")
    .WithTags("General")
    .WithSummary("API health check")
    .Produces<object>(StatusCodes.Status200OK)
    .WithOpenApi();

    app.Run();
}