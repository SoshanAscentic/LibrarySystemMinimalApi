using AutoMapper;
using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities.Members;
using LibrarySystemMinimalApi.Domain.Entities.Staff;
using Member = LibrarySystemMinimalApi.Domain.Entities.Members.Member;

namespace LibrarySystemMinimalApi.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;
        private readonly IMapper mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            this.memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public MemberDto AddMember(CreateMemberDto createMemberDto)
        {
            if (createMemberDto == null)
                throw new ArgumentNullException(nameof(createMemberDto));

            if (string.IsNullOrWhiteSpace(createMemberDto.Name))
                throw new ArgumentException("Member name cannot be null or empty.", nameof(createMemberDto.Name));

            // Don't generate ID manually - let EF Core handle it
            var member = CreateMember(createMemberDto.Name.Trim(), createMemberDto.MemberType);

            memberRepository.Add(member);
            return mapper.Map<MemberDto>(member);
        }

        public IEnumerable<MemberDto> GetAllMembers()
        {
            var members = memberRepository.GetAll();
            return mapper.Map<IEnumerable<MemberDto>>(members);
        }

        public MemberDto GetMemberById(int memberId)
        {
            if (memberId <= 0)
                throw new ArgumentException("Member ID must be positive.", nameof(memberId));

            var member = memberRepository.GetById(memberId);
            return member == null ? null : mapper.Map<MemberDto>(member);
        }

        private static Member CreateMember(string name, int memberType)
        {
            return memberType switch
            {
                0 => new RegularMember(name),           // No ID parameter
                1 => new MinorStaff(name),              // No ID parameter  
                2 => new ManagementStaff(name),         // No ID parameter
                _ => throw new ArgumentException($"Invalid member type: {memberType}. Valid types are 0 (Member), 1 (Minor Staff), 2 (Management Staff).")
            };
        }
    }
}