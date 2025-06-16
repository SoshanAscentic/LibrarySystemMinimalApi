using AutoMapper;
using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var existingBook = bookRepository.GetByTitleAndYear(createBookDto.Title, createBookDto.PublicationYear);
            if (existingBook != null)
                throw new InvalidOperationException("A book with the same title and publication year already exists.");

            var category = (Book.BookCategory)createBookDto.Category;
            var book = new Book(createBookDto.Title, createBookDto.Author, createBookDto.PublicationYear, category);

            bookRepository.Add(book);
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
    }
}
