using EdTech.Quiz.Application.DTOs;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IUserService
{
    Task<UserQuizHistoryDTO?> GetUserQuizHistoryAsync(int UserId);
    Task<bool> DeleteUserByIdAsync(int id);
    Task<ResponseDTO> UpdateUserAsync(UpdateUserDTO dto);
}
