using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Exceptions;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Domain.Entities;
using EdTech.Quiz.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EdTech.Quiz.Infrastructure.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task CreateQuestionAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();

    }

    public IQueryable<Question> GetQuestionsByQuizId(int QuizId)
    {
        return _context.QuizQuestions
                        .Where(u => u.QuizId == QuizId)
                        .Include(u => u.Question).ThenInclude(u => u.Options)
                        .Select(u => u.Question);
    }

    public async Task<bool> DoesQuestionAlreadyExists(string question, int id = 0)
    {
        return await _context.Questions.AnyAsync(u => (id == 0 || u.Id != id) && u.Text.Trim().ToLower() == question.Trim().ToLower());
    }

    public IQueryable<Question> GetQuestions()
    {
        return _context.Questions.Include(u => u.Options);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<bool> DeleteQuestionByIdAsync(int id)
    {
        Question question = await _context.Questions.FirstOrDefaultAsync(u => u.Id == id) ?? throw new QuestionNotFoundException();
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateQuestionAsync(UpdateQuestionDTO dto)
    {
        Question question = await _context.Questions.FirstOrDefaultAsync(u => u.Id == dto.Id) ?? throw new QuestionNotFoundException();

        if (await DoesQuestionAlreadyExists(dto.Text, dto.Id)) throw new QuestionAlreadyExistsException();

        IEnumerable<int> dboptionsId = question.Options.Select(u => u.Id);
        IEnumerable<int> optionsId = dto.Options.Select(u => u.Id);

        bool areEquals = new HashSet<int>(dboptionsId).SetEquals(optionsId);
        if (!areEquals) throw new OptionInvalidIdsException();

        if (!dboptionsId.Contains(dto.CorrectOptionId)) throw new OptionCorrectIdInvalidException();

        question.Text = dto.Text.Trim();
        foreach (Option dboption in question.Options)
        {
            OptionDTO option = dto.Options.First(u => u.Id == dboption.Id);
            dboption.Text = option.Text.Trim();
        }
        question.CorrectOptionId = dto.CorrectOptionId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<QuestionDTO> GetQuestionByIdAsync(int id)
    {
        return await _context.Questions.Where(u => u.Id == id).Select(u => new QuestionDTO()
        {

            Id = u.Id,
            Text = u.Text,
            Options = u.Options.Select(u => new OptionDTO()
            {
                Id = u.Id,
                Text = u.Text,
            }).ToList(),
            CorrectOption = u.Options.First(o => o.Id == u.CorrectOptionId).Text,
            CorrectOptionId = u.Options.First(o => o.Id == u.CorrectOptionId).Id
        }).FirstOrDefaultAsync() ?? throw new QuestionNotFoundException();
    }
}
