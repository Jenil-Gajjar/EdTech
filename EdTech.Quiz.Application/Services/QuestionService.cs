using System.Linq.Expressions;
using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Exceptions;
using EdTech.Quiz.Application.Helpers;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Services;

public class QuestionService : IQuestionService

{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    public async Task<int> CreateQuestionAsync(CreateQuestionDTO dto)
    {
        if (await _questionRepository.DoesQuestionAlreadyExists(dto.Text)) throw new QuestionAlreadyExistsException();

        Question question = new()
        {
            Text = dto.Text.Trim()
        };

        foreach (string optionText in dto.Options)
        {
            question.Options.Add(new() { Text = optionText });
        }
        await _questionRepository.CreateQuestionAsync(question);
        await _questionRepository.SaveChangesAsync();

        question.CorrectOptionId = ((List<Option>)question.Options)[dto.CorrectOptionIndex].Id;

        await _questionRepository.SaveChangesAsync();
        return question.Id;

    }

    public async Task<QuestionDTO> GetQuestionByIdAsync(int id)
    {
        return await _questionRepository.GetQuestionByIdAsync(id);
    }
    public PaginatedResult<QuestionDTO> GetRandomQuestions(int? quizId, PaginationDTO dto)
    {

        IQueryable<Question> query = quizId.HasValue ? _questionRepository.GetQuestionsByQuizId(quizId.Value) : _questionRepository.GetQuestions();

        if (query == null || !query.Any())
        {
            if (quizId.HasValue) throw new QuizInvalidIdException();
            else throw new RecordsNotFoundException();
        }

        Expression<Func<QuestionDTO, bool>>? filter = null;
        Expression<Func<QuestionDTO, object>>? order;
        if (!string.IsNullOrEmpty(dto.Filter))
        {
            filter = u => u.Text.Trim().ToLower().Contains(dto.Filter.Trim().ToLower());
        }

        order = dto.Order switch
        {
            "Text" => u => u.Text,
            _ => u => Guid.NewGuid(),
        };

        if (dto.Count.HasValue)
        {
            query = query.Take((int)dto.Count);
        }

        IQueryable<QuestionDTO> projectedQuery = query.Select(u => new QuestionDTO()
        {
            Id = u.Id,
            Text = u.Text,
            Options = u.Options.Select(x => new OptionDTO()
            {
                Id = x.Id,
                Text = x.Text
            }).ToList(),
            CorrectOption = u.Options.First(o => o.Id == u.CorrectOptionId).Text,
            CorrectOptionId = u.Options.First(o => o.Id == u.CorrectOptionId).Id
        });

        PaginatedResult<QuestionDTO> paginatedResult = PaginationHelper.Paginate(
            projectedQuery,
            dto.PageNumber,
            dto.PageSize,
            filter,
            order,
            dto.OrderByDescending
        );

        return paginatedResult;
    }

    public async Task<bool> DeleteQuestionByIdAsync(int id)
    {
        return await _questionRepository.DeleteQuestionByIdAsync(id);
    }

    public async Task<bool> UpdateQuestionAsync(UpdateQuestionDTO dto)
    {
        return await _questionRepository.UpdateQuestionAsync(dto);
    }

}
