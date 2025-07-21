using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Domain.Entities;
using EdTech.Quiz.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EdTech.Quiz.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesUserAlreadyExists(string name)
    {
        return await _context.Users.AnyAsync(u => u.UserName.Trim().ToLower() == name.Trim().ToLower());
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid)
    {
        return await _context.UserQuizAttempts.Where(u => u.UserId == Userid).Include(u => u.User).Include(u => u.Quiz).ToListAsync();
    }
    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;

    }
}
