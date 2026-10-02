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
    public class QuizAnswerConfiguration : IEntityTypeConfiguration<QuizAnswer>
    {
        public void Configure(EntityTypeBuilder<QuizAnswer> builder)
        {
            builder.ToTable("QuizAnswer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsCorrect)
                .IsRequired();

            builder.HasIndex(x => new { x.AttemptId, x.QuestionId })
                .IsUnique();

            builder.HasIndex(x => x.QuestionId);

            builder.HasIndex(x => x.SelectedChoiceId);

            builder.HasOne(x => x.Attempt)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SelectedChoice)
                .WithMany(x => x.QuizAnswers)
                .HasForeignKey(x => x.SelectedChoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
