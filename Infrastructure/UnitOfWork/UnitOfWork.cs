using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LMSDbContext _context;
        private IGenericRepositoryAsync<User>? _users;
        private IGenericRepositoryAsync<Quiz>? _quizzes;
        private IGenericRepositoryAsync<Question>? _questions;
        private IGenericRepositoryAsync<QuestionChoice>? _questionChoices;
        private IGenericRepositoryAsync<QuizQuestion>? _quizQuestions;
        private IGenericRepositoryAsync<QuizAttempt>? _quizAttempts;
        private IGenericRepositoryAsync<QuizAnswer>? _quizAnswers;
        public UnitOfWork(LMSDbContext context)
        {
            _context = context;
        }
        public IGenericRepositoryAsync<User> Users =>
            _users ??= new GenericRepositoryAsync<User>(_context);

        public IGenericRepositoryAsync<Quiz> Quizzes =>
            _quizzes ??= new GenericRepositoryAsync<Quiz>(_context);

        public IGenericRepositoryAsync<Question> Questions =>
            _questions ??= new GenericRepositoryAsync<Question>(_context);

        public IGenericRepositoryAsync<QuestionChoice> QuestionChoices =>
            _questionChoices ??= new GenericRepositoryAsync<QuestionChoice>(_context);

        public IGenericRepositoryAsync<QuizQuestion> QuizQuestions =>
            _quizQuestions ??= new GenericRepositoryAsync<QuizQuestion>(_context);

        public IGenericRepositoryAsync<QuizAttempt> QuizAttempts =>
            _quizAttempts ??= new GenericRepositoryAsync<QuizAttempt>(_context);

        public IGenericRepositoryAsync<QuizAnswer> QuizAnswers =>
            _quizAnswers ??= new GenericRepositoryAsync<QuizAnswer>(_context);
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
