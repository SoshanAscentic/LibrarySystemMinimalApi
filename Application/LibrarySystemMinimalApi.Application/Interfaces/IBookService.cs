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

        bool RemoveBook(int bookId);
        BookDto GetBook(int bookId);
        Task<bool> IsBookAvailableAsync(int bookId);

        IEnumerable<BookDto> GetAllBooks();
        IEnumerable<BookDto> GetBooksByCategory(string category);
        IEnumerable<BookDto> GetAvailableBooks();
        IEnumerable<BookDto> GetBooksByAuthor(string author);
        Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(string category);
    }
}
