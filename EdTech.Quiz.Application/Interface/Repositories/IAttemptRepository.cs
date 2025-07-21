using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IAttemptRepository
{
    Task<int> CreateAttemptAsync(UserQuizAttempt attempt);
    Task UpdateAttemptAsync(UserQuizAttempt attempt);
    Task<UserQuizAttempt?> GetUserQuizAttemptAsync(int UserId, int QuizId);
    Task<bool> HasUserAttemptedQuizAsync(StartQuizAttemptDTO dto);

}
