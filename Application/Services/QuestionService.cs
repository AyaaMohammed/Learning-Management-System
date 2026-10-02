using Application.DTOs.Questions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Service;
using Application.Interfaces.UserService;
using Application.Results;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class QuestionService  
    {
        private readonly IGenericRepositoryAsync<Question> _questionRepository;
        private readonly IGenericRepositoryAsync<QuestionChoice> _choiceRepository;
        private readonly IUserService _userService;

        public QuestionService(
            IGenericRepositoryAsync<Question> questionRepository,
            IGenericRepositoryAsync<QuestionChoice> choiceRepository,
            IUserService userService)
        {
            _questionRepository = questionRepository;
            _choiceRepository = choiceRepository;
            _userService = userService;
        }

    }
}
