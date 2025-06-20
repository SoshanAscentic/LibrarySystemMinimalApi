using AutoMapper;
using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using LibrarySystemMinimalApi.Domain.Entities.Members;
using LibrarySystemMinimalApi.Domain.Entities.Staff;
using Microsoft.Extensions.Logging;
using Member = LibrarySystemMinimalApi.Domain.Entities.Members.Member;

namespace LibrarySystemMinimalApi.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;
        private readonly IMapper mapper;
        private readonly ILogger<MemberService> logger;

        public MemberService(IMemberRepository memberRepository, IMapper mapper, ILogger<MemberService> logger)
        {
            this.memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public MemberDto AddMember(CreateMemberDto createMemberDto)
        {
            if (createMemberDto == null)
                throw new ArgumentNullException(nameof(createMemberDto));

            if (string.IsNullOrWhiteSpace(createMemberDto.Name))
                throw new ArgumentException("Member name cannot be null or empty.", nameof(createMemberDto.Name));

            try
            {
                logger.LogInformation("Creating member of type {MemberType} with name {Name}",
                    createMemberDto.MemberType, createMemberDto.Name);

                var member = CreateMember(createMemberDto.Name.Trim(), createMemberDto.MemberType);

                logger.LogInformation("Member entity created, adding to repository");

                memberRepository.Add(member);

                logger.LogInformation("Member added to repository successfully with ID: {MemberID}", member.MemberID);

                return mapper.Map<MemberDto>(member);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating member: {Name}, Type: {MemberType}",
                    createMemberDto.Name, createMemberDto.MemberType);
                throw;
            }
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
                0 => new RegularMember(name),
                1 => new MinorStaff(name),
                2 => new ManagementStaff(name),
                _ => throw new ArgumentException($"Invalid member type: {memberType}. Valid types are 0 (Member), 1 (Minor Staff), 2 (Management Staff).")
            };
        }
    }
}