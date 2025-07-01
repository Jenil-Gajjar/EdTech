using EdTech.Quiz.Application.Interface.Repositoriess;
using EdTech.Quiz.Domain.Entities;
using EdTech.Quiz.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EdTech.Quiz.Infrastructure.Repositories;
using Quiz = Domain.Entities.Quiz;

public class QuizRepository : IQuizRepository
{

    private readonly AppDbContext _context;

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddQuizAsync(Quiz quiz)
    {
        await _context.Quizzes.AddAsync(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Quiz>> GetAllQuizzesAsync()
    {
        return await _context.Quizzes.Include(u => u.QuizQuestions).ThenInclude(qq => qq.Question).ThenInclude(q => q.Options).ToListAsync();
    }


    public async Task<Quiz?> GetQuizByIdAsync(int Id)
    {

        return await _context.Quizzes.Include(q => q.QuizQuestions).ThenInclude(qq => qq.Question).FirstOrDefaultAsync(q => q.Id == Id);
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();

    }
}
