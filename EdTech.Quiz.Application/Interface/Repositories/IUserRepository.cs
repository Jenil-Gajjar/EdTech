using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IUserRepository
{
    Task<bool> DoesNameAlreadyExists(string name, int id = 0);
    Task<bool> DoesEmailAlreadyExists(string email, int id = 0);
    Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid);
    Task<bool> DeleteUserByIdAsync(int id);
    Task<ResponseDTO> UpdateUserAsync(UpdateUserDTO user);

}
