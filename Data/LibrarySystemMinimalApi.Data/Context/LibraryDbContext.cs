using LibrarySystemMinimalApi.Data.Configuration;
using LibrarySystemMinimalApi.Domain.Entities;
using LibrarySystemMinimalApi.Domain.Entities.Members;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
       
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Applying entity configurations
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new MemberConfiguration());

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed some initial books
            modelBuilder.Entity<Book>().HasData(
                new Book("The Great Gatsby", "F. Scott Fitzgerald", 1925, Book.BookCategory.Fiction)
                {
                    IsAvailable = true
                },
                new Book("To Kill a Mockingbird", "Harper Lee", 1960, Book.BookCategory.Fiction)
                {
                    IsAvailable = true
                },
                new Book("1984", "George Orwell", 1949, Book.BookCategory.Fiction)
                {
                    IsAvailable = true
                },
                new Book("A Brief History of Time", "Stephen Hawking", 1988, Book.BookCategory.History)
                {
                    IsAvailable = true
                },
                new Book("The Very Hungry Caterpillar", "Eric Carle", 1969, Book.BookCategory.Child)
                {
                    IsAvailable = true
                }
            );
        }
    }
}
