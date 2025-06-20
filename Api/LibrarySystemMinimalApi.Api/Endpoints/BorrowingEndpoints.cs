using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemMinimalApi.Api.Endpoints
{
    public static class BorrowingEndpoints
    {
        public static void MapBorrowingEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/borrowing")
                .WithTags("Borrowing")
                .WithOpenApi();

            // POST /api/borrowing/borrow
            group.MapPost("/borrow", async (
                [FromBody] BorrowReturnDto borrowDto,
                IBorrowingService borrowingService) =>
            {
                try
                {
                    var success = borrowingService.BorrowBook(borrowDto);
                    if (!success)
                        return Results.BadRequest("Unable to borrow book. Please check if book is available and member has borrowing permissions.");

                    return Results.Ok(new
                    {
                        message = "Book borrowed successfully!",
                        bookId = borrowDto.BookId,
                        memberId = borrowDto.MemberID
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while borrowing the book.");
                }
            })
            .WithName("BorrowBook")
            .WithSummary("Borrow a book from the library")
            .Produces<object>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            // POST /api/borrowing/return
            group.MapPost("/return", async (
                [FromBody] BorrowReturnDto returnDto,
                IBorrowingService borrowingService) =>
            {
                try
                {
                    var success = borrowingService.ReturnBook(returnDto);
                    if (!success)
                        return Results.BadRequest("Unable to return book. Please check if book is currently borrowed by this member.");

                    return Results.Ok(new
                    {
                        message = "Book returned successfully!",
                        bookId = returnDto.BookId,
                        memberId = returnDto.MemberID
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while returning the book.");
                }
            })
            .WithName("ReturnBook")
            .WithSummary("Return a book to the library")
            .Produces<object>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            // GET /api/borrowing/member/{memberId}
            group.MapGet("/member/{memberId}", async (
                int memberId,
                IMemberService memberService) =>
            {
                if (memberId <= 0)
                    return Results.BadRequest("Member ID must be positive.");

                try
                {
                    var member = memberService.GetMemberById(memberId);
                    if (member == null)
                        return Results.NotFound($"Member with ID {memberId} not found.");

                    return Results.Ok(new
                    {
                        memberId = member.MemberID,
                        memberName = member.Name,
                        memberType = member.MemberType,
                        borrowedBooksCount = member.BorrowedBooksCount,
                        canBorrowBooks = member.CanBorrowBooks
                    });
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while retrieving borrowing status.");
                }
            })
            .WithName("GetMemberBorrowingStatus")
            .WithSummary("Get borrowing status for a specific member")
            .Produces<object>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
