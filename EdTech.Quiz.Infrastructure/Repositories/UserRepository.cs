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
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesUserAlreadyExists(string name)
    {
        return await _context.Users.AnyAsync(u => u.Name.Trim().ToLower() == name.Trim().ToLower());
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
