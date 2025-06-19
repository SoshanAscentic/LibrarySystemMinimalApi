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

        //Usual methods for book management:
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

            // Check if book already exists
            var existingBook = bookRepository.GetByTitleAndYear(createBookDto.Title, createBookDto.PublicationYear);
            if (existingBook != null)
            {
                Console.WriteLine($"DEBUG: Book already exists!");
                throw new InvalidOperationException("A book with the same title and publication year already exists.");
            }

            Console.WriteLine($"DEBUG: Creating new book entity");
            var category = (Book.BookCategory)createBookDto.Category;
            var book = new Book(createBookDto.Title, createBookDto.Author, createBookDto.PublicationYear, category);

            Console.WriteLine($"DEBUG: Adding book to repository");
            bookRepository.Add(book);

            Console.WriteLine($"DEBUG: Book added successfully");
            return mapper.Map<BookDto>(book);
        }

        public bool RemoveBook(string title, int publicationYear)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            var book = bookRepository.GetByTitleAndYear(title, publicationYear);
            if (book == null)
                return false;

            if (!book.IsAvailable)
                throw new InvalidOperationException("Cannot remove a book that is currently borrowed.");

            bookRepository.Remove(book);
            return true;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = bookRepository.GetAll();
            return mapper.Map<IEnumerable<BookDto>>(books);
        }

        public BookDto GetBook(string title, int publicationYear)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            var book = bookRepository.GetByTitleAndYear(title, publicationYear);
            return book == null ? null : mapper.Map<BookDto>(book);
        }


        //Additional methods for book management:
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

        public async Task<bool> IsBookAvailableAsync(string title, int year)
        {
            var book = await bookRepository.GetByTitleAndYearAsync(title, year);
            return book?.IsAvailable ?? false;
        }
    }
}