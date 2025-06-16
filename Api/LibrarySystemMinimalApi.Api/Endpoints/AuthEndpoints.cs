using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemMinimalApi.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth").WithTags("Authentication");

            group.MapPost("/login", async (
                [FromBody] LoginDto loginDto,
                IAuthenticationService authService) =>
            {
               
                var member = authService.Login(loginDto);
                return member == null
                    ? Results.NotFound("Member not found.")
                    : Results.Ok(member);
            });
        }
    }
}
