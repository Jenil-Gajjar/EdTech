using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task SaveChangesAsync();

}
