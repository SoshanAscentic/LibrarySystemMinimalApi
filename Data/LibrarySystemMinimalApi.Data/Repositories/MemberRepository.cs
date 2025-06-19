using LibrarySystemMinimalApi.Data.Context;
using LibrarySystemMinimalApi.Data.InMemoryStorage;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities.Members;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRepository(LibraryDbContext context) : base(context) { }

        
        public override void Add(Member member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            base.Add(member);
            SaveChanges(); 
        }

        public override IEnumerable<Member> GetAll()
        {
            return dbSet
                .OrderBy(m => m.Name)
                .ToList();
        }

        // Your existing method with validation
        public Member GetByIdWithValidation(int memberId)
        {
            var member = GetById(memberId);
            if (member == null)
                throw new InvalidOperationException($"Member with ID {memberId} not found.");
            return member;
        }

        // Specific Member methods
        public IEnumerable<Member> GetByMemberType<TMember>() where TMember : Member
        {
            return dbSet.OfType<TMember>().ToList();
        }

        public IEnumerable<Member> GetMembersWithBorrowedBooks()
        {
            return Find(m => m.BorrowedBooksCount > 0);
        }

        public int GetNextMemberId()
        {
            var lastMember = dbSet
                .OrderByDescending(m => m.MemberID)
                .FirstOrDefault();

            return lastMember?.MemberID + 1 ?? 1;
        }
    }
}