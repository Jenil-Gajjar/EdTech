using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Helpers;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IQuestionService
{
    Task<int> CreateQuestionAsync(CreateQuestionDTO dto);
    PaginatedResult<QuestionDTO> GetRandomQuestionsByQuizId(int QuizId, PaginationDTO dto);
    PaginatedResult<QuestionDTO> GetRandomQuestions(PaginationDTO dto);
    Task<bool> DeleteQuestionByIdAsync(int id);
}
