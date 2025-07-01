using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;


namespace EdTech.Quiz.Application.Services;

public class AttemptService : IAttemptService
{
    private readonly IAttemptRepository _attemptRepository;
    private readonly IQuestionRepository _questionRepository;


    public AttemptService(IAttemptRepository attemptRepository, IQuestionRepository questionRepository)
    {
        _attemptRepository = attemptRepository;
        _questionRepository = questionRepository;
    }

    public async Task<List<UserQuizAttemptDTO>> GetUserQuizHistoryAsync(int UserId)
    {
        var attempts = await _attemptRepository.GetQuizAttemptsByIdAsync(UserId);
        return attempts.Select(u => new UserQuizAttemptDTO
        {
            Name = u.User.Name,
            QuizId = u.QuizId,
            QuizTitle = u.Quiz.Title,
            Score = u.Score,
            TimeTaken = u.CompletedAt!.Value - u.StartedAt
        }).ToList();
    }


    public async Task<QuizResultDTO> SubmitAttemptAsync(SubmitAttemptDTO dto)
    {
        var attempt = await _attemptRepository.GetCurrentAttemptAsync(QuizId: dto.QuizId, Userid: dto.UserId) ?? throw new Exception("Attempt Not Found!");

        int correct = 0;

        foreach (UserAnswerDTO answer in dto.Answers)
        {
            var question = await _questionRepository.GetQuestionById(answer.QuestionId);
            if (question != null)
                if (question.CorrectOptionId == answer.SelectedOptionId) correct++;

        }
        attempt.CompletedAt = DateTime.UtcNow;

        return new QuizResultDTO
        {
            Score = correct,
            TimeTaken = attempt.CompletedAt.Value - attempt.StartedAt
        };
    }

}
