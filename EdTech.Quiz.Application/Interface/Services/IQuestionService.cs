using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Helpers;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IQuestionService
{
    Task<int> CreateQuestionAsync(CreateQuestionDTO dto);
    Task<QuestionDTO?> GetQuestionByIdAsync(int id);

    PaginatedResult<QuestionDTO> GetRandomQuestions(int? QuizId, PaginationDTO dto);
    Task<bool> DeleteQuestionByIdAsync(int id);
    Task<bool> UpdateQuestionAsync(UpdateQuestionDTO dto);

}
