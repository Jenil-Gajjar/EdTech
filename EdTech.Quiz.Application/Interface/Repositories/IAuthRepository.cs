using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IAuthRepository
{
    public Task<ResponseDTO> CreateUser(User user);
    public Task<User?> GetUserByUsername(string Username);

}
