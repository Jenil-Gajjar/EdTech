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

    public async Task<Question?> GetQuestionById(int Id)
    {
        return await _context.Questions.Include(u => u.Options).FirstOrDefaultAsync(q => q.Id == Id);
    }

    public async Task<List<Question>> GetQuestionsByIds(List<int> ids)
    {
        return await _context.Questions.Include(q => q.Options).Where(q => ids.Contains(q.Id)).ToListAsync();
    }


    public async Task<List<Question>> GetRandomQuestionsAsync(int QuizId, int Count)
    {
        return await _context.QuizQuestions
                        .Where(u => u.QuizId == QuizId)
                        .Select(u => u.Question)
                        .Include(u => u.Options)
                        .Take(Count)
                        .OrderBy(x => Guid.NewGuid())
                        .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
