using AutoMapper;
using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Application.Interfaces;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMemberService memberService;
        private readonly IMemberRepository memberRepository;
        private readonly IMapper mapper;

        public AuthenticationService(
            IMemberService memberService,
            IMemberRepository memberRepository,
            IMapper mapper)
        {
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            this.memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public MemberDto Login(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            if (loginDto.MemberID <= 0)
                throw new ArgumentException("Member ID must be positive.", nameof(loginDto.MemberID));

            var member = memberRepository.GetById(loginDto.MemberID);
            if (member == null)
                return null;

            return mapper.Map<MemberDto>(member);
        }

        public MemberDto SignUp(CreateMemberDto createMemberDto)
        {
            if (createMemberDto == null)
                throw new ArgumentNullException(nameof(createMemberDto));

            return memberService.AddMember(createMemberDto);
        }
    }
}
