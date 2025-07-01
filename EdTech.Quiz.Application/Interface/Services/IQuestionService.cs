using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IQuestionService
{
    
    Task<int> CreateQuestionAsync(CreateQuestionDTO dto);

    Task<List<Question>> GetRandomQuestionsAsync(int QuizId, int Count);

}
