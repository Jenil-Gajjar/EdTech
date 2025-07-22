using System.Linq.Expressions;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Helpers;
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
        if (await _quizRepository.DoesQuizAlreadyExists(dto.Title)) throw new Exception("Quiz already exits.");

        if (!await _quizRepository.AreValidQuestionIds(dto.QuestionIds)) throw new Exception("Invalid question ids.");

        Quiz quiz = new()
        {
            Title = dto.Title.Trim()
        };
        foreach (int id in dto.QuestionIds)
        {
            quiz.QuizQuestions.Add(new() { QuestionId = id, Quiz = quiz });
        }
        await _quizRepository.CreateQuizAsync(quiz);
        await _quizRepository.SaveChangesAsync();

        return quiz.Id;
    }


    public PaginatedResult<QuizDTO> GetAllQuizzes(PaginationDTO dto)
    {

        IQueryable<Quiz> query = _quizRepository.GetAllQuizzes();
        Expression<Func<QuizDTO, bool>>? filter = null;
        Expression<Func<QuizDTO, object>>? order = null;

        if (!string.IsNullOrEmpty(dto.Filter))
        {
            filter = u => u.Title.Trim().ToLower().Contains(dto.Filter.Trim().ToLower());
        }

        order = dto.Order switch
        {
            "Title" => u => u.Title,
            _ => u => Guid.NewGuid(),
        };

        IQueryable<QuizDTO> projectedQuery = query.Select(u => new QuizDTO
        {
            Id = u.Id,
            Title = u.Title,
            Questions = u.QuizQuestions.Select(u => new QuestionDTO()
            {
                Id = u.Question.Id,
                Text = u.Question.Text,
                Options = u.Question.Options.Select(x => new OptionDTO()
                {
                    Id = x.Id,
                    Text = x.Text
                }).ToList(),
                CorrectOption = u.Question.Options.First(o => o.Id == u.Question.CorrectOptionId).Text,
                CorrectOptionId = u.Question.Options.First(o => o.Id == u.Question.CorrectOptionId).Id
            }).ToList()
        });
        PaginatedResult<QuizDTO> paginatedResult = PaginationHelper.Paginate(
            projectedQuery,
            dto.PageNumber,
            dto.PageSize,
            filter,
            order,
            dto.OrderByDescending
        );
        return paginatedResult;

    }

    public async Task<QuizDTO?> GetQuizByIdAsync(int id)
    {
        Quiz? result = await _quizRepository.GetQuizByIdAsync(id);
        if (result == null) return null;

        QuizDTO quiz = new()
        {
            Id = result.Id,
            Title = result.Title,
            Questions = result.QuizQuestions.Select(u => new QuestionDTO()
            {
                Id = u.QuestionId,
                Text = u.Question.Text,
                Options = u.Question.Options.Select(x => new OptionDTO()
                {
                    Id = x.Id,
                    Text = x.Text
                }).ToList(),
                CorrectOption = u.Question.Options.First(o => o.Id == u.Question.CorrectOptionId).Text,
                CorrectOptionId = u.Question.Options.First(o => o.Id == u.Question.CorrectOptionId).Id
            }).ToList()
        };
        return quiz;
    }


    public async Task<bool> DeleteQuizByIdAsync(int id)
    {
        return await _quizRepository.DeleteQuizByIdAsync(id);
    }

    public async Task<bool> UpdateQuizAsync(UpdateQuizDTO dto)
    {
        return await _quizRepository.UpdateQuizAsync(dto);
    }

}
