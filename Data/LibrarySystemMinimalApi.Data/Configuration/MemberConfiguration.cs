using LibrarySystemMinimalApi.Domain.Entities.Members;
using LibrarySystemMinimalApi.Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Primary Key
            builder.HasKey(m => m.MemberID);

            // Properties
            builder.Property(m => m.MemberID)
                .IsRequired()
                .ValueGeneratedOnAdd(); // Auto-increment

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.BorrowedBooksCount)
                .IsRequired()
                .HasDefaultValue(0);

            // Table Per Hierarchy (TPH) inheritance strategy
            builder.ToTable("Members");

            // Discriminator column to distinguish between member types
            builder.HasDiscriminator<string>("MemberType")
                .HasValue<RegularMember>("Member")
                .HasValue<MinorStaff>("MinorStaff")
                .HasValue<ManagementStaff>("ManagementStaff");

            // Index for better query performance
            builder.HasIndex(m => m.Name)
                .HasDatabaseName("IX_Members_Name");
        }
    }
}
