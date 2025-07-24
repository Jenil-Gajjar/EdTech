namespace EdTech.Quiz.Application.Interface.Repositoriess;

using EdTech.Quiz.Application.DTOs.Request;
using Quiz = Domain.Entities.Quiz;
public interface IQuizRepository
{
    Task CreateQuizAsync(Quiz quiz);
    Task<bool> AreValidQuestionIds(List<int> QuestionIds);
    Task<bool> DoesQuizAlreadyExists(string quiz, int id = 0);
    Task<Quiz> GetQuizByIdAsync(int id);
    IQueryable<Quiz> GetAllQuizzes();
    Task<bool> DeleteQuizByIdAsync(int id);
    Task SaveChangesAsync();
    Task<bool> UpdateQuizAsync(UpdateQuizDTO dto);
}
