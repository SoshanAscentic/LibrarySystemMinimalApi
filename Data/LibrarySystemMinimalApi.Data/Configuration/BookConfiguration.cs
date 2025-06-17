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
            //Primary Key : Composite key using Title and PublicationYear
            builder.HasKey(b => new { b.Title, b.PublicationYear }); 

            //Properties
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
                .HasConversion<int>(); // Store enum as int

            builder.Property(b => b.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);

            //Table configuration
            builder.ToTable("Books");

            //Index for better query performance
            builder.HasIndex(b => b.Author)
                .HasDatabaseName("IX_Books_Author");

            builder.HasIndex(b => b.Category)
                .HasDatabaseName("IX_Books_Category");
        }
    }
}
