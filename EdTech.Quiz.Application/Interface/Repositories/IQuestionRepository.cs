using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IQuestionRepository
{
    Task AddQuestionAsync(Question question);
    Task<bool> DoesQuestionAlreadyExists(string question);
    IQueryable<Question> GetQuestionsByQuizId(int QuizId);
    Task SaveChangesAsync();
    IQueryable<Question> GetQuestions();

}
