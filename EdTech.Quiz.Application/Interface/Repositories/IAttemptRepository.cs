using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IAttemptRepository
{
    Task AddAttemptAsync(UserQuizAttempt attempt);
    Task<UserQuizAttempt?> GetCurrentAttemptAsync(int QuizId, int Userid);

    Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid);
    Task SaveChangesAsync();
}
