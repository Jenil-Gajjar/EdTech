namespace EdTech.Quiz.Application.Interface.Repositoriess;

using Quiz = Domain.Entities.Quiz;
public interface IQuizRepository
{
    Task AddQuizAsync(Quiz quiz);
    Task<bool> AreValidQuestionIds(List<int> QuestionIds);
    Task<bool> DoesQuizAlreadyExists(string quiz);
    Task<Quiz?> GetQuizByIdAsync(int Id);
    Task<List<Quiz>> GetAllQuizzesAsync();
    Task SaveChangesAsync();

}
