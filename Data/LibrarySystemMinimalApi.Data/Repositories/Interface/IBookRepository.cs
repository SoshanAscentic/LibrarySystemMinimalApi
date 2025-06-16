using LibrarySystemMinimalApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories.Interface
{
    public interface IBookRepository
    {
        void Add(Book book);
        void Remove(Book book);
        Book GetByTitleAndYear(string title, int publicationYear);
        IEnumerable<Book> GetAll();
    }
}
