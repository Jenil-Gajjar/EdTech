using EdTech.Quiz.Application.DTOs;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IQuestionService
{
    Task<int> CreateQuestionAsync(CreateQuestionDTO dto);
    Task<List<QuestionDTO>> GetRandomQuestionsByQuizIdAsync(int QuizId, int Count);
    Task<List<QuestionDTO>> GetRandomQuestionsAsync(int Count);

}
