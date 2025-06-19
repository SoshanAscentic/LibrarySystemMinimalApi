using LibrarySystemMinimalApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories.Interface
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Book GetByTitleAndYear(string title, int publicationYear);
        Task<Book> GetByTitleAndYearAsync(string title, int publicationYear);
        IEnumerable<Book> GetByCategory(Book.BookCategory category);
        IEnumerable<Book> GetAvailableBooks();
        IEnumerable<Book> GetByAuthor(string author);
    }
}
