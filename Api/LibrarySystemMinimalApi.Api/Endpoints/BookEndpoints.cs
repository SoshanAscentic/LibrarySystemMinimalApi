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
            var group = app.MapGroup("/api/books").WithTags("Books");

            group.MapGet("/", GetAllBooks);
            group.MapGet("/{title}/{year}", GetBook);
            group.MapPost("/", AddBook);
            group.MapPost("/{title}/{year}", RemoveBook);

        }

        private static async Task<IResult> GetAllBooks(IBookService bookService)
        {
            try
            {
                var books = bookService.GetAllBooks();
                return Results.Ok(books);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        }

        private static async Task<IResult> GetBook(string title, int year, IBookService bookService)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Results.BadRequest("Title cannot be empty.");


            try
            {
                var book = bookService.GetBook(title, year);
                if (book == null)
                {
                    return Results.NotFound($"Book '{title}' ({year}) not found.");
                }
                return Results.Ok(book);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        }

        private static async Task<IResult> AddBook([FromBody] CreateBookDto createBookDto, IBookService bookService)
        {
            if (createBookDto == null)
                return Results.BadRequest("Invalid book data.");
            try
            {
                var book = bookService.AddBook(createBookDto);
                return Results.Created($"/api/books/{book.Title}/{book.PublicationYear}", book);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        }

        private static async Task<IResult> RemoveBook(string title, int year, IBookService bookService)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Results.BadRequest("Title cannot be empty.");
            try
            {
                var success = bookService.RemoveBook(title, year);
                if (!success)
                {
                    return Results.NotFound($"Book '{title}' ({year}) not found or cannot be removed.");
                }
                return Results.NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        }
    }
}
