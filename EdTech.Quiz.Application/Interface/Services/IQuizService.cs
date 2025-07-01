using EdTech.Quiz.Application.DTOs;

namespace EdTech.Quiz.Application.Interface.Services;

using Quiz = Domain.Entities.Quiz;
public interface IQuizService
{
    Task<int> CreateQuizAsync(CreateQuizDTO dto);


    Task<Quiz?> GetQuizByIdAsync(int Id);
    Task<List<Quiz>> GetAllQuizzesAsync();

}
