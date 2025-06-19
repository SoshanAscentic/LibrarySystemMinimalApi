using LibrarySystemMinimalApi.Data.Context;
using LibrarySystemMinimalApi.Data.InMemoryStorage;
using LibrarySystemMinimalApi.Data.Repositories;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with SQL server
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("LibrarySystemMinimalApi.Data")));

            // Register Generic Base Repository (optional, for direct injection)
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            // Register Specific Repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();

            return services;
        }
    }

}
