using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace LibrarySystemMinimalApi.Api.Endpoints
{
    public static class BookEndpoints
    {
        public static void MapBookEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/books")
                .WithTags("Books")
                .WithOpenApi();

            // GET /api/books
            group.MapGet("/", async (IBookService bookService) =>
            {
                try
                {
                    var books = bookService.GetAllBooks();
                    return Results.Ok(books);
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while retrieving books.");
                }
            })
            .WithName("GetAllBooks")
            .WithSummary("Get all books in the library")
            .Produces<IEnumerable<BookDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            // GET /api/books/{title}/{year}
            group.MapGet("/{title}/{year}", async (
                string title,
                int year,
                IBookService bookService) =>
            {
                if (string.IsNullOrWhiteSpace(title))
                    return Results.BadRequest("Title cannot be empty.");

                if (year < 1450 || year > DateTime.Now.Year)
                    return Results.BadRequest($"Publication year must be between 1450 and {DateTime.Now.Year}.");

                try
                {
                    var book = bookService.GetBook(title, year);
                    if (book == null)
                        return Results.NotFound($"Book '{title}' ({year}) not found.");

                    return Results.Ok(book);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while retrieving the book.");
                }
            })
            .WithName("GetBook")
            .WithSummary("Get a specific book by title and publication year")
            .Produces<BookDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            // POST /api/books
            group.MapPost("/", async (
                [FromBody] CreateBookDto createBookDto,
                IBookService bookService) =>
            {
                try
                {
                    var book = bookService.AddBook(createBookDto);
                    return Results.Created($"/api/books/{book.Title}/{book.PublicationYear}", book);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(ex.Message);
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while adding the book.");
                }
            })
            .WithName("AddBook")
            .WithSummary("Add a new book to the library")
            .Produces<BookDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);

            // DELETE /api/books/{title}/{year}
            group.MapDelete("/{title}/{year}", async (
                string title,
                int year,
                IBookService bookService) =>
            {
                if (string.IsNullOrWhiteSpace(title))
                    return Results.BadRequest("Title cannot be empty.");

                if (year < 1450 || year > DateTime.Now.Year)
                    return Results.BadRequest($"Publication year must be between 1450 and {DateTime.Now.Year}.");

                try
                {
                    var success = bookService.RemoveBook(title, year);
                    if (!success)
                        return Results.NotFound($"Book '{title}' ({year}) not found.");

                    return Results.NoContent();
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
                    return Results.Problem("An error occurred while removing the book.");
                }
            })
            .WithName("RemoveBook")
            .WithSummary("Remove a book from the library")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
