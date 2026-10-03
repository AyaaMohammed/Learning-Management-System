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
        Task<Result<IReadOnlyList<QuestionResponseDto>>> GetAllAsync();

        Task<Result<QuestionResponseDto>> GetByIdAsync(Guid id);

        Task<Result<Guid>> CreateAsync(QuestionRequest request);

        Task<Result<Guid>> UpdateAsync(Guid id, QuestionRequest request);

        Task<Result<bool>> DeleteAsync(Guid id);
    }
}
