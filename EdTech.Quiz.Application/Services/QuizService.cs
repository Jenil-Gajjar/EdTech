using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Repositoriess;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;
namespace EdTech.Quiz.Application.Services;
using Quiz = Domain.Entities.Quiz;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;

    public QuizService(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<int> CreateQuizAsync(CreateQuizDTO dto)
    {
        Quiz quiz = new()
        {
            Title = dto.Title
        };
        foreach (int id in dto.QuestionIds)
        {
            quiz.QuizQuestions.Add(new() { QuestionId = id, Quiz = quiz });
        }
        await _quizRepository.AddQuizAsync(quiz);
        await _quizRepository.SaveChangesAsync();

        return quiz.Id;
    }

    public async Task<List<Quiz>> GetAllQuizzesAsync()
    {
        return await _quizRepository.GetAllQuizzesAsync();
    }


    public async Task<Quiz?> GetQuizByIdAsync(int Id)
    {
        return await _quizRepository.GetQuizByIdAsync(Id);
    }


   
}
