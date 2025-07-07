using EdTech.Quiz.Domain.Entities;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EdTech.Quiz.Application.DTOs;

namespace EdTech.Quiz.Infrastructure.Repositories;

public class AttemptRepository : IAttemptRepository
{
    private readonly AppDbContext _context;

    public AttemptRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<int> AddAttemptAsync(UserQuizAttempt attempt)
    {
        await _context.UserQuizAttempts.AddAsync(attempt);
        await _context.SaveChangesAsync();
        return attempt.Id;
    }
    public async Task EditAttemptAsync(UserQuizAttempt attempt)
    {
        _context.UserQuizAttempts.Update(attempt);
        await _context.SaveChangesAsync();
    }

    public async Task<UserQuizAttempt?> GetUserQuizAttemptAsync(int UserId, int QuizId)
    {
        return await _context.UserQuizAttempts.Include(u => u.User).FirstOrDefaultAsync(u => u.UserId == UserId && u.QuizId == QuizId);
    }
  
    public async Task<bool> HasUserAttemptedQuizAsync(StartQuizAttemptDTO dto)
    {
        return await _context.UserQuizAttempts.AnyAsync(u => u.UserId == dto.UserId && u.QuizId == dto.QuizId);
    }

}
