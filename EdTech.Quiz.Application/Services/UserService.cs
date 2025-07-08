using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;
using static EdTech.Quiz.Application.DTOs.UserQuizHistoryDTO;

namespace EdTech.Quiz.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }



    public async Task<int> CreateUserAsync(CreateUserDTO dto)
    {
        if (await _userRepository.DoesUserAlreadyExists(dto.Name)) throw new Exception("Name already exists");

        User user = new() { Name = dto.Name.Trim() };
        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();
        return user.Id;

    }

    public async Task<UserQuizHistoryDTO?> GetUserQuizHistoryAsync(int UserId)
    {
        List<UserQuizAttempt> attempts = await _userRepository.GetQuizAttemptsByIdAsync(UserId);

        List<QuizDetails> Quiz = attempts.Select(u => new QuizDetails()
        {
            Title = u.Quiz.Title,
            Score = u.Score,
            TimeTaken = u.CompletedAt == null ? TimeSpan.Zero : u.CompletedAt.Value - u.StartedAt
        }).ToList();

        return new UserQuizHistoryDTO()
        {
            Name = attempts.First().User.Name,
            Quizzes = Quiz
        };
    }


}
