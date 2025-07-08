using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IQuestionRepository
{
    Task AddQuestionAsync(Question question);
    Task<bool> DoesQuestionAlreadyExists(string question);
    Task<List<Question>> GetQuestionsByQuizIdAsync(int QuizId);
    Task SaveChangesAsync();
    Task<List<Question>> GetQuestionsAsync();

}
