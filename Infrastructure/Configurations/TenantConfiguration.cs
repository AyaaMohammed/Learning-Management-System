using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenant");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasData(
                    new Tenant
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Name = "Tenant 1",
                        CreatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc),
                        UpdatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc)
                    },
                    new Tenant
                    {
                        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        Name = "Tenant 2",
                        CreatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc),
                        UpdatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc)
                    },
                    new Tenant
                    {
                        Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                        Name = "Tenant 3",
                        CreatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc),
                        UpdatedAt = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc)
                    }
                );
        }
    }
}
