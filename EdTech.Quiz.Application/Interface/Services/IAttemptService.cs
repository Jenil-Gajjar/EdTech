using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;

namespace EdTech.Quiz.Application.Interface.Services;

public interface IAttemptService
{
    Task<QuizResultDTO> SubmitAttemptAsync(UserQuizAttemptDTO dto);
    Task<int> StartAttemptAsync(StartQuizAttemptDTO dto);

}

