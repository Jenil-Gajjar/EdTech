using EdTech.Quiz.Application.DTOs;

namespace EdTech.Quiz.Application.Interface.Services;
public interface IQuizService
{
    Task<int> CreateQuizAsync(CreateQuizDTO dto);
    Task<List<QuizDTO>> GetAllQuizzesAsync();
    Task<QuizDTO> GetQuizByIdAsync(int Id);

}
