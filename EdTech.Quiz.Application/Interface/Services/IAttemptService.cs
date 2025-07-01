using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IAttemptService
{

    Task<List<UserQuizAttemptDTO>> GetUserQuizHistoryAsync(int UserId);
    Task<QuizResultDTO> SubmitAttemptAsync(SubmitAttemptDTO dto);
}

