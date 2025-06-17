using LibrarySystemMinimalApi.Data.Context;
using LibrarySystemMinimalApi.Data.InMemoryStorage;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext context;

        public BookRepository(LibraryDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            context.Books.Add(book);
            context.SaveChanges();
        }

        public void Remove(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            context.Books.Remove(book);
            context.SaveChanges();
        }

        public Book GetByTitleAndYear (string title, int publicationYear) 
        {
            var book = context.Books.FirstOrDefault(b =>
            EF.Functions.Like(b.Title, title) &&
            b.PublicationYear == publicationYear);

            if (book == null)
                throw new InvalidOperationException($"No book found with title '{title}' and year {publicationYear}.");

            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            return context.Books
                .OrderBy(b => b.Title)
                .ThenBy(b => b.PublicationYear)
                .ToList();
        }
    }
}
