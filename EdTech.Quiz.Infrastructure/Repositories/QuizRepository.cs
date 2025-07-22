using EdTech.Quiz.Application.DTOs;
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
    public async Task CreateQuizAsync(Quiz quiz)
    {
        await _context.Quizzes.AddAsync(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesQuizAlreadyExists(string quiz, int id = 0)
    {
        return await _context.Quizzes.AnyAsync(q => (id == 0 || q.Id != id) && q.Title.Trim().ToLower() == quiz.Trim().ToLower());
    }

    public async Task<bool> AreValidQuestionIds(List<int> QuestionIds)
    {
        if (QuestionIds == null || !QuestionIds.Any()) return false;

        return await _context.Questions.Where(q => QuestionIds.Contains(q.Id)).CountAsync() == QuestionIds.Count;
    }
    public IQueryable<Quiz> GetAllQuizzes()
    {
        return _context.Quizzes.Include(u => u.QuizQuestions).ThenInclude(qq => qq.Question).ThenInclude(q => q.Options);
    }

    public async Task<Quiz?> GetQuizByIdAsync(int id)
    {
        return await _context.Quizzes.AsSplitQuery().Include(q => q.QuizQuestions).ThenInclude(qq => qq.Question).ThenInclude(q => q.Options).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<bool> DeleteQuizByIdAsync(int id)
    {
        Quiz? quiz = await _context.Quizzes.FirstOrDefaultAsync(u => u.Id == id);
        if (quiz == null) return false;

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateQuizAsync(UpdateQuizDTO dto)
    {
        Quiz? quiz = await _context.Quizzes.Include(u => u.QuizQuestions).FirstOrDefaultAsync(u => u.Id == dto.Id);
        if (quiz == null) return false;

        if (await DoesQuizAlreadyExists(dto.Title, dto.Id)) throw new Exception("Quiz already exists.");

        if (!await AreValidQuestionIds(dto.QuestionIds)) throw new Exception("Invalid question ids.");

        quiz.Title = dto.Title.Trim();

        IEnumerable<int> dbQuestionIds = quiz.QuizQuestions.Select(q => q.QuestionId);

        HashSet<int> insertIds = dto.QuestionIds.Except(dbQuestionIds).ToHashSet();
        IEnumerable<int> deleteIds = dbQuestionIds.Except(dto.QuestionIds);

        if (insertIds.Any())
        {
            IEnumerable<QuizQuestion> insertEntites = insertIds.Select(id => new QuizQuestion()
            {
                QuizId = dto.Id,
                QuestionId = id
            });
            await _context.QuizQuestions.AddRangeAsync(insertEntites);
        }

        if (deleteIds.Any())
        {
            List<QuizQuestion> deleteEntities = await _context.QuizQuestions.Where(u => u.QuizId == dto.Id && deleteIds.Contains(u.QuestionId)).ToListAsync();
            _context.QuizQuestions.RemoveRange(deleteEntities);
        }
        await _context.SaveChangesAsync();
        return true;
    }
}
