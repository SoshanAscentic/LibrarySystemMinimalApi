using LibrarySystemMinimalApi.Domain.Entities.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories.Interface
{
    public interface IMemberRepository
    {
        void Add(Member member);
        Member GetById(int memberId);
        IEnumerable<Member> GetAll();
        int GetNextMemberId();
    }
}
