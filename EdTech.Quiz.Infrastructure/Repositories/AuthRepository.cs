using EdTech.Quiz.Application.DTOs;
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
        if (await DoesEmailExists(user.Email))
        {
            return new()
            {
                IsSuccess = false,
                Message = "Email already exists."
            };
        }
        if (await DoesUserNameExists(user.UserName))
        {
            return new()
            {
                IsSuccess = false,
                Message = "Username already exists."
            };
        }
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return new()
        {
            IsSuccess = true,
            Message = "User created successfully."
        };


    }
}
