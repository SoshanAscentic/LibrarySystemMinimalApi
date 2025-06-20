using LibrarySystemMinimalApi.Data.Context;
using LibrarySystemMinimalApi.Data.InMemoryStorage;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        private readonly ILogger<MemberRepository> logger;

        public MemberRepository(LibraryDbContext context, ILogger<MemberRepository> logger) : base(context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void Add(Member member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            try
            {
                logger.LogInformation("Adding member to database: {Name}", member.Name);

                base.Add(member);
                SaveChanges(); // This calls context.SaveChanges()

                logger.LogInformation("Member added successfully with ID: {MemberID}", member.MemberID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding member to database: {Name}", member.Name);
                throw;
            }
        }

        public override IEnumerable<Member> GetAll()
        {
            return dbSet
                .OrderBy(m => m.Name)
                .ToList();
        }

        public Member GetByIdWithValidation(int memberId)
        {
            var member = GetById(memberId);
            if (member == null)
                throw new InvalidOperationException($"Member with ID {memberId} not found.");
            return member;
        }

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