using LibrarySystemMinimalApi.Domain.Entities.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories.Interface
{
    public interface IMemberRepository : IBaseRepository<Member>
    {
        // Specific methods for Member entity
        Member GetByIdWithValidation(int memberId);
        IEnumerable<Member> GetByMemberType<TMember>() where TMember : Member;
        IEnumerable<Member> GetMembersWithBorrowedBooks();
        int GetNextMemberId();
    }
}
