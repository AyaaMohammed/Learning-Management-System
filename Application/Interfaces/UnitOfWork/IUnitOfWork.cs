using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepositoryAsync<User> Users { get; }
        IGenericRepositoryAsync<Quiz> Quizzes { get; }
        IGenericRepositoryAsync<Question> Questions { get; }
        IGenericRepositoryAsync<QuestionChoice> QuestionChoices { get; }
        IGenericRepositoryAsync<QuizQuestion> QuizQuestions { get; }
        IGenericRepositoryAsync<QuizAttempt> QuizAttempts { get; }
        IGenericRepositoryAsync<QuizAnswer> QuizAnswers { get; }
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

        Task<int> SaveChangesAsync();
    }
}
