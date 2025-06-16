using LibrarySystemMinimalApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.Interfaces
{
    public interface IAuthenticationService
    {
        MemberDto Login(LoginDto loginDto);
        MemberDto SignUp(CreateMemberDto createMemberDto);
    }
}
