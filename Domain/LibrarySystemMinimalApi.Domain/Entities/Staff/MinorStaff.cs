using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Domain.Entities.Staff
{
    public class MinorStaff : Staff
    {
        // Parameterless constructor for EF Core
        public MinorStaff() : base(string.Empty) { }

        public MinorStaff(string name) : base(name) { }

        public override string GetMemberType() => "Minor Staff";
    }
}
