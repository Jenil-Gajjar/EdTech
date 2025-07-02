using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IQuestionRepository
{
    Task AddQuestionAsync(Question question);

    Task<List<Question>> GetQuestionsByQuizIdAsync(int QuizId);

    Task SaveChangesAsync();

    Task<List<Question>> GetQuestionsAsync();


}
