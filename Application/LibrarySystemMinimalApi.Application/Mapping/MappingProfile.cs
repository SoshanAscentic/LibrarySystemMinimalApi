using AutoMapper;
using AutoMapper.Execution;
using LibrarySystemMinimalApi.Application.DTOs;
using LibrarySystemMinimalApi.Domain.Entities;
using LibrarySystemMinimalApi.Domain.Entities.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book mappings
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));

            // Member mappings
            CreateMap<Domain.Entities.Members.Member, MemberDto>()
                .ForMember(dest => dest.MemberType, opt => opt.MapFrom(src => src.GetMemberType()))
                .ForMember(dest => dest.CanBorrowBooks, opt => opt.MapFrom(src => src.CanBorrowBooks()))
                .ForMember(dest => dest.CanViewBooks, opt => opt.MapFrom(src => src.CanViewBooks()))
                .ForMember(dest => dest.CanViewMembers, opt => opt.MapFrom(src => src.CanViewMembers()))
                .ForMember(dest => dest.CanManageBooks, opt => opt.MapFrom(src => src.CanManageBooks()));
        }
    }
}
