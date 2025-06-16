using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Domain.Entities.Staff
{
    public class MinorStaff : Staff
    {
        public MinorStaff(string name, int memberId) : base(name, memberId) { }
        public override string GetMemberType() => "Minor Staff";
    }
}
