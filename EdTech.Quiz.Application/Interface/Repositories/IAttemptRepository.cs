using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IAttemptRepository
{
    Task<int> AddAttemptAsync(UserQuizAttempt attempt);
    Task EditAttemptAsync(UserQuizAttempt attempt);
    Task<UserQuizAttempt?> GetUserQuizAttemptAsync(int UserId, int QuizId);
    Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid);
    Task<bool> HasUserAttemptedQuizAsync(StartQuizAttemptDTO dto);


}
