using LibrarySystemMinimalApi.Data.Context;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemMinimalApi.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context) { }

        
        public override void Add(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            base.Add(book);
            SaveChanges(); 
        }

        public override void Remove(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            base.Remove(book);
            SaveChanges(); 
        }

        public override IEnumerable<Book> GetAll()
        {
            return dbSet
                .OrderBy(b => b.Title)
                .ThenBy(b => b.PublicationYear)
                .ToList();
        }

        // Specific Book methods
        public Book GetByTitleAndYear(string title, int publicationYear)
        {
            return GetFirstOrDefault(b =>
                b.Title == title &&
                b.PublicationYear == publicationYear);
        }

        public async Task<Book> GetByTitleAndYearAsync(string title, int publicationYear)
        {
            return await GetFirstOrDefaultAsync(b =>
                b.Title == title &&
                b.PublicationYear == publicationYear);
        }

        public IEnumerable<Book> GetByCategory(Book.BookCategory category)
        {
            return Find(b => b.Category == category);
        }

        public IEnumerable<Book> GetAvailableBooks()
        {
            return Find(b => b.IsAvailable);
        }

        public IEnumerable<Book> GetByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return Enumerable.Empty<Book>();

            return Find(b => b.Author.Contains(author));
        }
    }
}