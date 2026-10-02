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
    public class QuestionChoiceConfiguration: IEntityTypeConfiguration<QuestionChoice>
    {
        public void Configure(EntityTypeBuilder<QuestionChoice> builder)
        {
            builder.ToTable("QuestionChoice");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.IsCorrect)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.QuestionId);
        }
    }
}
