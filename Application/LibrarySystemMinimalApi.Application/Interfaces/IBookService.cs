using LibrarySystemMinimalApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.Interfaces
{
    public interface IBookService
    {
        BookDto AddBook(CreateBookDto createBookDto);
        bool RemoveBook(string title, int publicationYear);
        IEnumerable<BookDto> GetAllBooks();
        BookDto GetBook(string title, int publicationYear);
        IEnumerable<BookDto> GetBooksByCategory(string category);
        IEnumerable<BookDto> GetAvailableBooks();
        IEnumerable<BookDto> GetBooksByAuthor(string author);
        Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(string category);
        Task<bool> IsBookAvailableAsync(string title, int year);
    }
}
