using LibrarySystemMinimalApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories.Interface
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Book GetByBookId(int bookId);
        Task<Book> GetByBookIdAsync(int bookId);
        IEnumerable<Book> GetByCategory(Book.BookCategory category);
        IEnumerable<Book> GetAvailableBooks();
        IEnumerable<Book> GetByAuthor(string author);
    }
}
