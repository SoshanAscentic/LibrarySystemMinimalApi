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

            // Check for duplicate title + year combination
            var existingBook = GetByTitleAndYear(book.Title, book.PublicationYear);
            if (existingBook != null)
            {
                throw new InvalidOperationException("A book with the same title and publication year already exists.");
            }

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

        public Book GetByBookId(int bookId)
        {
            return GetById(bookId);
        }

        public async Task<Book> GetByBookIdAsync(int bookId)
        {
            return await GetByIdAsync(bookId);
        }

        // Existing Title + Year methods (kept for backwards compatibility)
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