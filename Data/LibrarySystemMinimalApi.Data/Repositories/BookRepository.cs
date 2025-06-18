using LibrarySystemMinimalApi.Data.Context;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities;

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

        public Book GetByTitleAndYear(string title, int publicationYear)
        {
            // Use direct string comparison instead of EF.Functions.Like
            return context.Books.FirstOrDefault(b =>
                b.Title == title &&  // Direct comparison
                b.PublicationYear == publicationYear);
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