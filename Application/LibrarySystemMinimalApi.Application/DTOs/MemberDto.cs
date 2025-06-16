using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.DTOs
{
    public class MemberDto
    {
        public string? Name { get; set; }
        public int MemberID { get; set; }
        public string? MemberType { get; set; }
        public int BorrowedBooksCount { get; set; }
        public bool CanBorrowBooks { get; set; }
        public bool CanViewBooks { get; set; }
        public bool CanViewMembers { get; set; }
        public bool CanManageBooks { get; set; }
    }
}
