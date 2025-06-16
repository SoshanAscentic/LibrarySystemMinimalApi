using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Domain.Entities.Members
{
    public abstract class Member
    {
        public string Name { get; set; }
        public int MemberID { get; private set; }
        public int BorrowedBooksCount { get; set; } = 0;

        protected Member(string name, int memberId)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name cannot be empty") : name;
            MemberID = memberId > 0 ? memberId : throw new ArgumentException("Member ID must be positive");
        }

        public abstract string GetMemberType();
        public virtual bool CanBorrowBooks() => false;
        public virtual bool CanViewBooks() => false;
        public virtual bool CanViewMembers() => false;
        public virtual bool CanManageBooks() => false;

        public override string ToString()
        {
            return $"Name: {Name}, ID: {MemberID}, Type: {GetMemberType()}, Borrowed Books: {BorrowedBooksCount}";
        }

    }
}
