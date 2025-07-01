namespace EdTech.Quiz.Application.Interface.Repositoriess;

using EdTech.Quiz.Domain.Entities;

using Quiz = Domain.Entities.Quiz;
public interface IQuizRepository
{
    Task AddQuizAsync(Quiz quiz);

    Task<Quiz?> GetQuizByIdAsync(int Id);

    Task<List<Quiz>> GetAllQuizzesAsync();

    Task SaveChangesAsync();

}
