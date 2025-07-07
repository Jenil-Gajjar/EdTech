using EdTech.Quiz.Application.Interface.Repositoriess;
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

    public async Task<bool> DoesQuizAlreadyExists(string quiz)
    {
        return await _context.Quizzes.AnyAsync(q => q.Title.Trim().ToLower() == quiz.Trim().ToLower());
    }

    public async Task<bool> AreValidQuestionIds(List<int> QuestionIds)
    {
        if (QuestionIds == null || !QuestionIds.Any()) return false;

        return await _context.Questions.Where(q => QuestionIds.Contains(q.Id)).CountAsync() == QuestionIds.Count;
    }
    public async Task<List<Quiz>> GetAllQuizzesAsync()
    {
        return await _context.Quizzes.Include(u => u.QuizQuestions).ThenInclude(qq => qq.Question).ThenInclude(q => q.Options).ToListAsync();
    }

    public async Task<Quiz?> GetQuizByIdAsync(int Id)
    {
        return await _context.Quizzes.AsSplitQuery().Include(q => q.QuizQuestions).ThenInclude(qq => qq.Question).ThenInclude(q => q.Options).FirstOrDefaultAsync(q => q.Id == Id);
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();

    }
}
