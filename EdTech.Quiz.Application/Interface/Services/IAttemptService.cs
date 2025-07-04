using EdTech.Quiz.Application.DTOs;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IAttemptService
{

    Task<UserQuizHistoryDTO?> GetUserQuizHistoryAsync(int UserId);
    Task<QuizResultDTO> SubmitAttemptAsync(UserQuizAttemptDTO dto);
    Task<int> StartAttemptAsync(StartQuizAttemptDTO dto);

}

