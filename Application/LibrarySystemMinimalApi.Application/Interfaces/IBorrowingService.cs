using LibrarySystemMinimalApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.Interfaces
{
    public interface IBorrowingService
    {
        bool BorrowBook(int bookId, int memberId);
        bool ReturnBook(int bookId, int memberId);

    }
}
