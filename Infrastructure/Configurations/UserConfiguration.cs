using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(x => new { x.TenantId, x.Email })
                .IsUnique();

            builder.HasIndex(x => x.TenantId);

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                 new User
                 {
                     Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                     TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                     Name = "admin",
                     Email = "admin@tenant1.com",
                     PasswordHash = "ECCXVLl1yRr/akfekaujGiWwjWD3QFa4m1ojq1fSZg4xMjM0",
                     PasswordSalt = "1234",
                     Role = UserRole.Admin,
                     IsActive = true,
                     CreatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc),
                     UpdatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc)
                 },
                 new User
                 {
                     Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                     TenantId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                     Name = "student",
                     Email = "student@tenant2.com",
                     PasswordHash = "ECCXVLl1yRr/akfekaujGiWwjWD3QFa4m1ojq1fSZg4xMjM0",
                     PasswordSalt = "1234",
                     Role = UserRole.Student,
                     IsActive = true,
                     CreatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc),
                     UpdatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc)
                 }
             );
        }
    }
}
