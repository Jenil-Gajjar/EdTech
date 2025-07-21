namespace EdTech.Quiz.Application.Interface.Repositoriess;

using Quiz = Domain.Entities.Quiz;
public interface IQuizRepository
{
    Task CreateQuizAsync(Quiz quiz);
    Task<bool> AreValidQuestionIds(List<int> QuestionIds);
    Task<bool> DoesQuizAlreadyExists(string quiz);
    Task<Quiz?> GetQuizByIdAsync(int id);
    IQueryable<Quiz> GetAllQuizzes();
    Task<bool> DeleteQuizByIdAsync(int id);
    Task SaveChangesAsync();

}
