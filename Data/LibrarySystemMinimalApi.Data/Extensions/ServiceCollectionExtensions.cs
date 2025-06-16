using LibrarySystemMinimalApi.Data.InMemoryStorage;
using LibrarySystemMinimalApi.Data.Repositories;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<DataStorage>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();

            return services;
        }
    }
}
