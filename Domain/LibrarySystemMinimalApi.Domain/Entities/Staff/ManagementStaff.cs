using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Domain.Entities.Staff
{
    public class ManagementStaff : Staff
    {
        // Parameterless constructor for EF Core
        public ManagementStaff() : base(string.Empty) { }

        public ManagementStaff(string name) : base(name) { }

        public override string GetMemberType() => "Management Staff";
        public override bool CanBorrowBooks() => true;
    }
}
