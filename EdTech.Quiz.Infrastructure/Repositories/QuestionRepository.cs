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

    public async Task<bool> DoesQuestionAlreadyExists(string question)
    {
        return await _context.Questions.AnyAsync(u => u.Text.Trim().ToLower() == question.Trim().ToLower());
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
        Question? question = await _context.Questions.FirstOrDefaultAsync(u => u.Id == id);
        if (question == null) return false;
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return true;
    }

}
