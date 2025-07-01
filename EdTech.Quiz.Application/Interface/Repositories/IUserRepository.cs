using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserByIdAsync(int Id);
    Task SaveChangesAsync();

}
