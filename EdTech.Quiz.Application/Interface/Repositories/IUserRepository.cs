using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task SaveChangesAsync();
    Task<bool> DoesUserAlreadyExists(string name);
    Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid);

}
