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
    public async Task AddQuestionAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();

    }


    public async Task<List<Question>> GetQuestionsByQuizIdAsync(int QuizId)
    {
        return await _context.QuizQuestions
                        .Where(u => u.QuizId == QuizId)
                        .Include(u => u.Question).ThenInclude(u => u.Options)
                        .Select(u => u.Question)
                        .ToListAsync();
    }

    public async Task<bool> DoesQuestionAlreadyExists(string question)
    {
        return await _context.Questions.AnyAsync(u => u.Text.Trim().ToLower() == question.Trim().ToLower());
    }

    public async Task<List<Question>> GetQuestionsAsync()
    {
        return await _context.Questions.Include(u => u.Options).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
