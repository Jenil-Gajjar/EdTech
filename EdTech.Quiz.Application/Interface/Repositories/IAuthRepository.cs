using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IAuthRepository
{
    public Task<ResponseDTO> CreateUser(User user);
    public Task<User?> GetUserByUsername(string Username);

}
