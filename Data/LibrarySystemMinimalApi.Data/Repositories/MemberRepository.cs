using LibrarySystemMinimalApi.Data.InMemoryStorage;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DataStorage dataStorage;

        public MemberRepository(DataStorage dataStorage)
        {
            this.dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        public void Add(Member member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            dataStorage.Members.Add(member);
        }

        public Member GetById(int memberId)
        {
            return dataStorage.Members.FirstOrDefault(m => m.MemberID == memberId);
        }

        public IEnumerable<Member> GetAll()
        {
            return dataStorage.Members.AsReadOnly();
        }

        public int GetNextMemberId()
        {
            return dataStorage.NextMemberId++;
        }
    }
}
