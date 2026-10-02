using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants => Set<Tenant>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Question> Questions => Set<Question>();

        public DbSet<QuestionChoice> QuestionChoices => Set<QuestionChoice>();

        public DbSet<Quiz> Quizzes => Set<Quiz>();

        public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();

        public DbSet<QuizAttempt> QuizAttempts => Set<QuizAttempt>();

        public DbSet<QuizAnswer> QuizAnswers => Set<QuizAnswer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(LMSDbContext).Assembly);
        }
    }
}
