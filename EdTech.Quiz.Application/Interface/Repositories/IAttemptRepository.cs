using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Interface.Repositories;

public interface IAttemptRepository
{
    Task AddAttemptAsync(UserQuizAttempt attempt);
    Task<List<UserQuizAttempt>> GetQuizAttemptsByIdAsync(int Userid);
    Task<bool> HasUserAttemptedQuizAsync(UserQuizAttemptDTO dto);


}
