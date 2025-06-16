using LibrarySystemMinimalApi.Application.Interfaces;
using LibrarySystemMinimalApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping.MappingProfile));

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IBorrowingService, BorrowingService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
