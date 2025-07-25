using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Exceptions;
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
    public async Task<bool> DoesNameAlreadyExists(string name, int id = 0)
    {
        return await _context.Users.AnyAsync(u => (id == 0 || u.Id != id) && u.UserName.Trim().ToLower() == name.Trim().ToLower());
    }
    public async Task<bool> DoesEmailAlreadyExists(string email, int id = 0)
    {
        return await _context.Users.AnyAsync(u => (id == 0 || u.Id != id) && u.Email.Trim().ToLower() == email.Trim().ToLower());
    }
    public async Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid)
    {
        return await _context.UserQuizAttempts.Where(u => u.UserId == Userid).Include(u => u.User).Include(u => u.Quiz).ToListAsync();
    }
    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new UserNotFoundException();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<ResponseDTO> UpdateUserAsync(UpdateUserDTO userDTO)
    {
        if (await DoesNameAlreadyExists(userDTO.Username, userDTO.Id)) throw new UsernameAlreadyExistsException();

        if (await DoesEmailAlreadyExists(userDTO.Email, userDTO.Id)) throw new EmailAlreadyExistsException();

        User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDTO.Id) ?? throw new UserNotFoundException();

        user.UserName = userDTO.Username;
        user.Email = userDTO.Email;
        user.Password = userDTO.Password;

        await _context.SaveChangesAsync();
        return new()
        {
            Data = userDTO.Username,
            IsSuccess = true,
            Message = "User updated successfully."
        };

    }
}
