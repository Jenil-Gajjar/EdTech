using EdTech.Quiz.Application.DTOs;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IUserService
{
    Task<int> CreateUserAsync(CreateUserDTO dto);

    Task<UserQuizHistoryDTO?> GetUserQuizHistoryAsync(int UserId);

}
