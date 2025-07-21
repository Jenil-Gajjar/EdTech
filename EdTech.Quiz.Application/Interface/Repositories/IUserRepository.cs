using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IUserRepository
{
    Task CreateUserAsync(User user);
    Task SaveChangesAsync();
    Task<bool> DoesUserAlreadyExists(string name);
    Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid);
    Task<bool> DeleteUserByIdAsync(int id);

}
