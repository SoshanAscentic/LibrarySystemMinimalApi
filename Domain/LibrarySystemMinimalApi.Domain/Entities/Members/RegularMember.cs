using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Domain.Entities.Members
{
    public class RegularMember : Member
    {

        // Parameterless constructor for EF Core
        public RegularMember() { }

        public RegularMember(string name, int memberId) : base(name, memberId) { }

        public override string GetMemberType() => "Member";
        public override bool CanBorrowBooks() => true;
        public override bool CanViewBooks() => true;
        public override bool CanViewMembers() => true;
    }
}
