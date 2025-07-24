using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Exceptions;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Application.Response.DTOs;
using EdTech.Quiz.Domain.Entities;
using static EdTech.Quiz.Application.Response.DTOs.UserQuizHistoryDTO;

namespace EdTech.Quiz.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserQuizHistoryDTO> GetUserQuizHistoryAsync(int UserId)
    {
        List<UserQuizAttempt> attempts = await _userRepository.GetQuizAttemptsByIdAsync(UserId);

        if (attempts == null || !attempts.Any())
            throw new AttemptsNotFoundException();

        List<QuizDetails> Quiz = attempts.Select(u => new QuizDetails()
        {
            Title = u.Quiz.Title,
            Score = u.Score,
            TimeTaken = u.CompletedAt == null ? TimeSpan.Zero : u.CompletedAt.Value - u.StartedAt
        }).ToList();
        return new UserQuizHistoryDTO()
        {
            Name = attempts.First().User.UserName,
            Quizzes = Quiz
        };
    }


    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        return await _userRepository.DeleteUserByIdAsync(id);
    }

    public async Task<ResponseDTO> UpdateUserAsync(UpdateUserDTO dto)
    {
        dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        return await _userRepository.UpdateUserAsync(dto);
    }
}
