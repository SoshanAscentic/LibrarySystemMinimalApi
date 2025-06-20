using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemMinimalApi.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth")
                .WithTags("Authentication")
                .WithOpenApi();

            // POST /api/auth/login
            group.MapPost("/login", async (
                [FromBody] LoginDto loginDto,
                IAuthenticationService authService,
                ILogger<Program> logger) =>
            {
                try
                {
                    var member = authService.Login(loginDto);
                    if (member == null)
                        return Results.NotFound("Member not found. Please sign up first.");

                    return Results.Ok(member);
                }
                catch (ArgumentException ex)
                {
                    logger.LogWarning("Login validation error: {Message}", ex.Message);
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during login for MemberID: {MemberID}", loginDto?.MemberID);
                    return Results.Problem("An error occurred during login.");
                }
            })
            .WithName("Login")
            .WithSummary("Authenticate a member by Member ID")
            .Produces<MemberDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            // POST /api/auth/signup
            group.MapPost("/signup", async (
                [FromBody] CreateMemberDto createMemberDto,
                IAuthenticationService authService,
                ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Attempting to create member: {Name}, Type: {MemberType}",
                        createMemberDto?.Name, createMemberDto?.MemberType);

                    var member = authService.SignUp(createMemberDto);

                    logger.LogInformation("Member created successfully with ID: {MemberID}", member.MemberID);

                    return Results.Created($"/api/members/{member.MemberID}", member);
                }
                catch (ArgumentException ex)
                {
                    logger.LogWarning("Signup validation error: {Message}", ex.Message);
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during signup for member: {Name}, Type: {MemberType}",
                        createMemberDto?.Name, createMemberDto?.MemberType);
                    return Results.Problem("An error occurred during signup.");
                }
            })
            .WithName("SignUp")
            .WithSummary("Register a new member")
            .Produces<MemberDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
