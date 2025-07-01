using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IQuestionRepository
{
    Task AddQuestionAsync(Question question);
    Task<List<Question>> GetQuestionsByIds(List<int> ids);

    Task<List<Question>> GetRandomQuestionsAsync(int QuizId, int Count);

    Task<Question?> GetQuestionById(int id);
    Task SaveChangesAsync();


}
