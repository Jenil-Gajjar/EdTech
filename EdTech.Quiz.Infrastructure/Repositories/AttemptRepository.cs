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
    public async Task AddAttemptAsync(UserQuizAttempt attempt)
    {
        await _context.UserQuizAttempts.AddAsync(attempt);
        await _context.SaveChangesAsync();
    }

    public async Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid)
    {
        return await _context.UserQuizAttempts.Where(u => u.UserId == Userid).Include(u => u.User).Include(u => u.Quiz).ToListAsync();
    }
    public async Task<bool> HasUserAttemptedQuizAsync(UserQuizAttemptDTO dto)
    {
        return  await _context.UserQuizAttempts.AnyAsync(u=>u.UserId == dto.UserId  && u.QuizId == dto.QuizId);
    }

}
