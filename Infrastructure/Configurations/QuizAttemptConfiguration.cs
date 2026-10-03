using Domain.Entities;
using Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class QuizAttemptConfiguration
        : IEntityTypeConfiguration<QuizAttempt>
    {
        public void Configure(EntityTypeBuilder<QuizAttempt> builder)
        {
            builder.ToTable("QuizAttempt");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Score)
                .HasDefaultValue(0);

            builder.Property(x => x.Percentage)
                .HasPrecision(5, 2)
                .HasDefaultValue(0);

            builder.Property(x => x.TotalQuestions)
                .IsRequired();

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.HasIndex(x => new { x.TenantId, x.StudentId });

            builder.HasIndex(x => new { x.TenantId, x.QuizId });

            builder.HasIndex(x => new { x.StudentId, x.QuizId });

            builder.HasIndex(x => x.SubmittedAt);

            builder.HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Quiz)
                .WithMany(x => x.Attempts)
                .HasForeignKey(x => x.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.Attempts)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedData.QuizAttempts);
        }
    }
}
