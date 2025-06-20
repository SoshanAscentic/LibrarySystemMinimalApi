using AutoMapper;
using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using LibrarySystemMinimalApi.Data.Repositories;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities;

namespace LibrarySystemMinimalApi.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public BookDto AddBook(CreateBookDto createBookDto)
        {
            if (createBookDto == null)
                throw new ArgumentNullException(nameof(createBookDto));

            Console.WriteLine($"DEBUG: Adding book - Title: '{createBookDto.Title}', Year: {createBookDto.PublicationYear}");

            var category = (Book.BookCategory)createBookDto.Category;
            var book = new Book(createBookDto.Title, createBookDto.Author, createBookDto.PublicationYear, category);

            Console.WriteLine($"DEBUG: Adding book to repository");
            bookRepository.Add(book);

            Console.WriteLine($"DEBUG: Book added successfully");
            return mapper.Map<BookDto>(book);
        }

        public bool RemoveBook(int bookId)
        {
            var book = bookRepository.GetByBookId(bookId);
            if (book == null)
                return false;

            if (!book.IsAvailable)
                throw new InvalidOperationException("Cannot remove a book that is currently borrowed.");

            bookRepository.Remove(book);
            return true;
        }

        public BookDto GetBook(int bookId)
        {
            var book = bookRepository.GetByBookId(bookId);
            return book == null ? null : mapper.Map<BookDto>(book);
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = await bookRepository.GetByBookIdAsync(bookId);
            return book?.IsAvailable ?? false;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = bookRepository.GetAll();
            return mapper.Map<IEnumerable<BookDto>>(books);
        }

        public IEnumerable<BookDto> GetBooksByCategory(string category)
        {
            if (!Enum.TryParse<Book.BookCategory>(category, true, out var bookCategory))
                throw new ArgumentException($"Invalid category: {category}. Valid categories are: Fiction, History, Child");

            var books = bookRepository.GetByCategory(bookCategory);
            return mapper.Map<IEnumerable<BookDto>>(books);
        }

        public IEnumerable<BookDto> GetAvailableBooks()
        {
            var books = bookRepository.GetAvailableBooks();
            return mapper.Map<IEnumerable<BookDto>>(books);
        }

        public IEnumerable<BookDto> GetBooksByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be null or empty.", nameof(author));

            var books = bookRepository.GetByAuthor(author);
            return mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(string category)
        {
            if (!Enum.TryParse<Book.BookCategory>(category, true, out var bookCategory))
                throw new ArgumentException($"Invalid category: {category}");

            var books = bookRepository.GetByCategory(bookCategory);
            return mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
}