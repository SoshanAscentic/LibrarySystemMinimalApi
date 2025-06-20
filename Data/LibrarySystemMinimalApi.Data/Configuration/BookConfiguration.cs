using LibrarySystemMinimalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Primary Key is now BookId (auto-incrementing)
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.BookId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Properties
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.PublicationYear)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(b => b.Category)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(b => b.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);

            // Table configuration
            builder.ToTable("Books");

            // Indexes
            builder.HasIndex(b => b.Author)
                .HasDatabaseName("IX_Books_Author");

            builder.HasIndex(b => b.Category)
                .HasDatabaseName("IX_Books_Category");

            // Unique constraint for Title + PublicationYear combination
            builder.HasIndex(b => new { b.Title, b.PublicationYear })
                .IsUnique()
                .HasDatabaseName("IX_Books_Title_Year");

            // Seed data with NEGATIVE BookId values (EF Core requirement)
            builder.HasData(
                new Book
                {
                    BookId = -1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    PublicationYear = 1925,
                    Category = Book.BookCategory.Fiction,
                    IsAvailable = true
                },
                new Book
                {
                    BookId = -2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    PublicationYear = 1960,
                    Category = Book.BookCategory.Fiction,
                    IsAvailable = true
                },
                new Book
                {
                    BookId = -3,
                    Title = "1984",
                    Author = "George Orwell",
                    PublicationYear = 1949,
                    Category = Book.BookCategory.Fiction,
                    IsAvailable = true
                },
                new Book
                {
                    BookId = -4,
                    Title = "A Brief History of Time",
                    Author = "Stephen Hawking",
                    PublicationYear = 1988,
                    Category = Book.BookCategory.History,
                    IsAvailable = true
                },
                new Book
                {
                    BookId = -5,
                    Title = "The Very Hungry Caterpillar",
                    Author = "Eric Carle",
                    PublicationYear = 1969,
                    Category = Book.BookCategory.Child,
                    IsAvailable = true
                }
            );
        }
    }
}