using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Helpers;

namespace EdTech.Quiz.Application.Interface.Services;
public interface IQuizService
{
    Task<int> CreateQuizAsync(CreateQuizDTO dto);
    PaginatedResult<QuizDTO> GetAllQuizzes(PaginationDTO dto);
    Task<QuizDTO?> GetQuizByIdAsync(int id);
    Task<bool> DeleteQuizByIdAsync(int id);
    Task<bool> UpdateQuizAsync(UpdateQuizDTO dto);

}
