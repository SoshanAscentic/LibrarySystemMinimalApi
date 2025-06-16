using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;

namespace LibrarySystemMinimalApi.Api.Endpoints
{
    public static class MemberEndpoints
    {
        public static void MapMemberEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/members")
                .WithTags("Members")
                .WithOpenApi();

            // GET /api/members
            group.MapGet("/", async (IMemberService memberService) =>
            {
                try
                {
                    var members = memberService.GetAllMembers();
                    return Results.Ok(members);
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while retrieving members.");
                }
            })
            .WithName("GetAllMembers")
            .WithSummary("Get all members in the library")
            .Produces<IEnumerable<MemberDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);


            // GET /api/members/{memberId}
            group.MapGet("/{memberId}", async (
                int memberId,
                IMemberService memberService) =>
            {
                if (memberId <= 0)
                    return Results.BadRequest("Member ID must be a positive integer.");
                try
                {
                    var member = memberService.GetMemberById(memberId);
                    if (member == null)
                        return Results.NotFound($"Member with ID {memberId} not found.");
                    return Results.Ok(member);
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while retrieving the member.");
                }
            })
            .WithName("GetMemberById")
            .WithSummary("Get a member by Member ID")
            .Produces<MemberDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
