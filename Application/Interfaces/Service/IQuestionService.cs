using Application.DTOs.Questions;
using Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IQuestionService
    {
        Task<Result<Guid>> AddAsync(CreateQuestionRequest request);
    }
}
