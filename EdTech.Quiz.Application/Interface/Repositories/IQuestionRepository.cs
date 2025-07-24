using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IQuestionRepository
{
    Task CreateQuestionAsync(Question question);
    Task<bool> DoesQuestionAlreadyExists(string question, int id = 0);
    IQueryable<Question> GetQuestionsByQuizId(int QuizId);
    Task SaveChangesAsync();
    IQueryable<Question> GetQuestions();
    Task<bool> DeleteQuestionByIdAsync(int id);
    Task<bool> UpdateQuestionAsync(UpdateQuestionDTO dto);
    Task<QuestionDTO> GetQuestionByIdAsync(int id);
}
