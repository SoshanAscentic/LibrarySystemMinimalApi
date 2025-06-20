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

        // BookId-based methods
        public bool BorrowBook(int bookId, int memberId)
        {
            var member = memberRepository.GetById(memberId);
            if (member == null)
                throw new InvalidOperationException($"Member with ID {memberId} not found.");

            if (!member.CanBorrowBooks())
                throw new InvalidOperationException($"{member.GetMemberType()} does not have permission to borrow books.");

            var book = bookRepository.GetByBookId(bookId);
            if (book == null)
                throw new InvalidOperationException($"Book with ID {bookId} not found.");

            if (!book.IsAvailable)
                throw new InvalidOperationException($"Book '{book.Title}' is currently not available for borrowing.");

            const int MAX_BORROWED_BOOKS = 5;
            if (member.BorrowedBooksCount >= MAX_BORROWED_BOOKS)
                throw new InvalidOperationException($"Member has reached the maximum borrowing limit of {MAX_BORROWED_BOOKS} books.");

            // Update book and member state
            book.IsAvailable = false;
            member.BorrowedBooksCount++;

            return true;
        }

        public bool ReturnBook(int bookId, int memberId)
        {
            var member = memberRepository.GetById(memberId);
            if (member == null)
                throw new InvalidOperationException($"Member with ID {memberId} not found.");

            var book = bookRepository.GetByBookId(bookId);
            if (book == null)
                throw new InvalidOperationException($"Book with ID {bookId} not found.");

            if (book.IsAvailable)
                throw new InvalidOperationException($"Book '{book.Title}' is not currently borrowed.");

            if (member.BorrowedBooksCount <= 0)
                throw new InvalidOperationException("Member has no borrowed books to return.");

            book.IsAvailable = true;
            member.BorrowedBooksCount--;

            return true;
        }

        public bool ReturnBook(BorrowReturnDto returnDto)
        {
            if (returnDto == null)
                throw new ArgumentNullException(nameof(returnDto));


            if (returnDto.BookId > 0)
            {
                return ReturnBook(returnDto.BookId, returnDto.MemberID);
            }

            throw new ArgumentException("Either BookId or Title + PublicationYear must be provided.");
        }
    }
}
