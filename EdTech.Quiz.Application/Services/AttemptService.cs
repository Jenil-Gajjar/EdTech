using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Exceptions;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;


namespace EdTech.Quiz.Application.Services;

public class AttemptService : IAttemptService
{
    private readonly IAttemptRepository _attemptRepository;
    private readonly IQuestionRepository _questionRepository;

    private readonly TimeSpan MaxDuration = TimeSpan.FromMinutes(30);

    public AttemptService(IAttemptRepository attemptRepository, IQuestionRepository questionRepository)
    {
        _attemptRepository = attemptRepository;
        _questionRepository = questionRepository;
    }



    public async Task<int> StartAttemptAsync(StartQuizAttemptDTO dto)
    {
        if (await _attemptRepository.HasUserAttemptedQuizAsync(dto)) throw new UserAlreadyAttemptedQuizException();

        UserQuizAttempt attempt = new()
        {
            UserId = dto.UserId,
            QuizId = dto.QuizId,
            StartedAt = DateTime.UtcNow
        };
        return await _attemptRepository.CreateAttemptAsync(attempt);
    }

    private bool IsAttemptWithinTimeLimit(UserQuizAttempt attempt)
    {
        TimeSpan? duration = attempt.CompletedAt - attempt.StartedAt;
        return duration <= MaxDuration;
    }

    public async Task<QuizResultDTO> SubmitAttemptAsync(UserQuizAttemptDTO dto)
    {

        IQueryable<Question> questions = _questionRepository.GetQuestionsByQuizId(dto.QuizId) ?? throw new QuizNotFoundException();

        UserQuizAttempt attempt = await _attemptRepository.GetUserQuizAttemptAsync(UserId: dto.UserId, QuizId: dto.QuizId) ?? throw new QuizNotStartedException();

        IEnumerable<int> ids = dto.Answers.Select(u => u.QuestionId);
        bool areEquals = new HashSet<int>(ids).SetEquals(questions.Select(u => u.Id));
        if (!areEquals) throw new QuestionInvalidIdsException();

        if (attempt.CompletedAt is not null) throw new QuizAlreadyCompletedException();
        attempt.CompletedAt = DateTime.UtcNow;

        if (!IsAttemptWithinTimeLimit(attempt)) throw new QuizAttemptTimeLimitException();

        attempt.Score = CalculateScore(attempt, dto, questions.ToList());

        await _attemptRepository.UpdateAttemptAsync(attempt);

        return new QuizResultDTO
        {

            AttemptId = attempt.Id,
            Name = attempt.User.UserName,
            QuizId = dto.QuizId,
            UserId = dto.UserId,
            Score = attempt.Score,
            TimeTaken = attempt.CompletedAt.Value - attempt.StartedAt
        };
    }
    public static double CalculateScore(UserQuizAttempt attempt, UserQuizAttemptDTO dto, List<Question> questions)
    {
        int correct = 0;
        foreach (UserAnswerDTO answer in dto.Answers)
        {
            Question? question = questions.FirstOrDefault(u => u.Id == answer.QuestionId);
            if (question == null) continue;

            if (question.CorrectOptionId == answer.SelectedOptionId) correct++;
            attempt.Answers.Add(new UserAnswer
            {
                QuestionId = answer.QuestionId,
                SelectedOptionId = answer.SelectedOptionId
            });
        }

        return Math.Round((double)correct / dto.Answers.Count * 100, 2);
    }
}
