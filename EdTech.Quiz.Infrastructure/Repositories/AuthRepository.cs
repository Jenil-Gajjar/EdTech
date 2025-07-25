using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Exceptions;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Domain.Entities;
using EdTech.Quiz.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EdTech.Quiz.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;
    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }


    private async Task<bool> DoesEmailExists(string Email)
    {
        return await _context.Users.AnyAsync(u => u.Email.Trim().ToLower() == Email.Trim().ToLower());
    }

    private async Task<bool> DoesUserNameExists(string UserName)
    {
        return await _context.Users.AnyAsync(u => u.UserName.Trim().ToLower() == UserName.Trim().ToLower());
    }

    public async Task<User?> GetUserByUsername(string Username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName.Trim() == Username.Trim());
    }

    public async Task<ResponseDTO> CreateUser(User user)
    {
        if (await DoesEmailExists(user.Email)) throw new EmailAlreadyExistsException();

        if (await DoesUserNameExists(user.UserName)) throw new UsernameAlreadyExistsException();

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return new()
        {
            Data = user.UserName,
            IsSuccess = true,
            Message = "User created successfully."
        };
    }
}
