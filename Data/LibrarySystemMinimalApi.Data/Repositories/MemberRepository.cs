using LibrarySystemMinimalApi.Data.Context;
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
        private readonly LibraryDbContext context;

        public MemberRepository(LibraryDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Member member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            context.Members.Add(member);
            context.SaveChanges();
        }

        public Member GetById(int memberId)
        {
            // Return null instead of throwing exception when member not found
            return context.Members.FirstOrDefault(m => m.MemberID == memberId);
        }

        public IEnumerable<Member> GetAll()
        {
            return context.Members
                .OrderBy(m => m.Name)
                .ToList();
        }

        public int GetNextMemberId()
        {
            //Since auto increment is being used i am not sure that if this is needed....
            var lastMember = context.Members
                .OrderByDescending(m => m.MemberID)
                .FirstOrDefault();

            return lastMember?.MemberID + 1 ?? 1;
        }
    }
}