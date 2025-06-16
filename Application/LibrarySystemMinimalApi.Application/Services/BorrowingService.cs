using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBookRepository bookRepository;
        private readonly IMemberRepository memberRepository;

        public BorrowingService(IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        }

        public bool BorrowBook(BorrowReturnDto borrowDto)
        {
            if (borrowDto == null)
                throw new ArgumentNullException(nameof(borrowDto));

            if (string.IsNullOrWhiteSpace(borrowDto.Title))
                throw new ArgumentException("Book title cannot be null or empty.", nameof(borrowDto.Title));

            var member = memberRepository.GetById(borrowDto.MemberID);
            if (member == null)
                throw new InvalidOperationException($"Member with ID {borrowDto.MemberID} not found.");

            if (!member.CanBorrowBooks())
                throw new InvalidOperationException($"{member.GetMemberType()} does not have permission to borrow books.");

            var book = bookRepository.GetByTitleAndYear(borrowDto.Title, borrowDto.PublicationYear);
            if (book == null)
                throw new InvalidOperationException($"Book '{borrowDto.Title}' ({borrowDto.PublicationYear}) not found.");

            if (!book.IsAvailable)
                throw new InvalidOperationException($"Book '{borrowDto.Title}' is currently not available for borrowing.");

            const int MAX_BORROWED_BOOKS = 5;
            if (member.BorrowedBooksCount >= MAX_BORROWED_BOOKS)
                throw new InvalidOperationException($"Member has reached the maximum borrowing limit of {MAX_BORROWED_BOOKS} books.");

            // Update book and member state
            book.IsAvailable = false;
            member.BorrowedBooksCount++;

            return true;
        }

        public bool ReturnBook(BorrowReturnDto returnDto)
        {
            if (returnDto == null)
                throw new ArgumentNullException(nameof(returnDto));

            if (string.IsNullOrWhiteSpace(returnDto.Title))
                throw new ArgumentException("Book title cannot be null or empty.", nameof(returnDto.Title));

            var member = memberRepository.GetById(returnDto.MemberID);
            if (member == null)
                throw new InvalidOperationException($"Member with ID {returnDto.MemberID} not found.");

            var book = bookRepository.GetByTitleAndYear(returnDto.Title, returnDto.PublicationYear);
            if (book == null)
                throw new InvalidOperationException($"Book '{returnDto.Title}' ({returnDto.PublicationYear}) not found.");

            if (book.IsAvailable)
                throw new InvalidOperationException($"Book '{returnDto.Title}' is not currently borrowed.");

            if (member.BorrowedBooksCount <= 0)
                throw new InvalidOperationException("Member has no borrowed books to return.");

            book.IsAvailable = true;
            member.BorrowedBooksCount--;

            return true;
        }
    }
}
