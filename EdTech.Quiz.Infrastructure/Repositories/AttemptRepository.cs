using EdTech.Quiz.Domain.Entities;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EdTech.Quiz.Infrastructure.Repositories;

public class AttemptRepository : IAttemptRepository
{
    private readonly AppDbContext _context;

    public AttemptRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAttemptAsync(UserQuizAttempt attempt)
    {
        await _context.UserQuizAttempts.AddAsync(attempt);
        await _context.SaveChangesAsync();
    }

    public async Task<UserQuizAttempt?> GetCurrentAttemptAsync(int QuizId, int Userid)
    {
        return await _context.UserQuizAttempts.FirstOrDefaultAsync(uq => uq.QuizId == QuizId && uq.UserId == Userid);
    }

    public async Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid)
    {
        return await _context.UserQuizAttempts.Include(u => u.User).Include(u => u.Quiz).Where(u => u.UserId == Userid).ToListAsync();
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
