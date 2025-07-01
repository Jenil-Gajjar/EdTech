using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IUserService
{
    Task<int> CreateUserAsync(CreateUserDTO dto);
    Task<User?> GetUserByIdAsync(int Id);

}
