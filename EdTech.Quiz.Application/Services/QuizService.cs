using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Repositoriess;
using EdTech.Quiz.Application.Interface.Services;
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
        if (await _quizRepository.DoesQuizAlreadyExists(dto.Title)) throw new Exception("Quiz Already Exits");

        if (!await _quizRepository.AreValidQuestionIds(dto.QuestionIds)) throw new Exception("Invalid Question Ids");

        Quiz quiz = new()
        {
            Title = dto.Title.Trim()
        };
        foreach (int id in dto.QuestionIds)
        {
            quiz.QuizQuestions.Add(new() { QuestionId = id, Quiz = quiz });
        }
        await _quizRepository.AddQuizAsync(quiz);
        await _quizRepository.SaveChangesAsync();

        return quiz.Id;
    }

    public async Task<List<QuizDTO>> GetAllQuizzesAsync()
    {

        return (await _quizRepository.GetAllQuizzesAsync()).Select(u => new QuizDTO
        {
            Id = u.Id,
            Title = u.Title,
            Questions = u.QuizQuestions.Select(u => new QuestionDTO()
            {
                Id = u.Question.Id,
                Text = u.Question.Text,
                Options = u.Question.Options.Select(u => u.Text).ToList(),
                CorrectOption = u.Question.Options.First(o => o.Id == u.Question.CorrectOptionId).Text,
                CorrectOptionId = u.Question.Options.First(o => o.Id == u.Question.CorrectOptionId).Id
            }).ToList()
        }).ToList();

    }

    public async Task<QuizDTO> GetQuizByIdAsync(int Id)
    {
        Quiz result = await _quizRepository.GetQuizByIdAsync(Id) ?? throw new Exception("Quiz Not Found");

        QuizDTO quiz = new()
        {
            Id = result.Id,
            Title = result.Title,
            Questions = result.QuizQuestions.Select(u => new QuestionDTO()
            {
                Id = u.QuestionId,
                Text = u.Question.Text,
                Options = u.Question.Options.Select(u => u.Text).ToList(),
                CorrectOption = u.Question.Options.First(o => o.Id == u.Question.CorrectOptionId).Text,
                CorrectOptionId = u.Question.Options.First(o => o.Id == u.Question.CorrectOptionId).Id
            }).ToList()
        };
        return quiz;
    }



}
