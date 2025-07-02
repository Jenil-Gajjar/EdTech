using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;


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

    public async Task<UserQuizHistoryDTO?> GetUserQuizHistoryAsync(int UserId)
    {
        List<UserQuizAttempt> attempts = await _attemptRepository.GetQuizAttemptsByIdAsync(UserId);

        List<QuizDTO> Quiz = attempts.Select(u => new QuizDTO()
        {
            Title = u.Quiz.Title,
            Score = u.Score
        }).ToList();

        return new UserQuizHistoryDTO()
        {
            Name = attempts.First().User.Name,
            Quizzes = Quiz
        };
    }


    public async Task<QuizResultDTO> SubmitAttemptAsync(UserQuizAttemptDTO dto)
    {
        if (await _attemptRepository.HasUserAttemptedQuizAsync(dto)) throw new Exception("User has already attempted this quiz");

        List<Question> questions = await _questionRepository.GetQuestionsByQuizIdAsync(dto.QuizId) ?? throw new Exception("Quiz Not Found");


        UserQuizAttempt attempt = new()
        {
            UserId = dto.UserId,
            QuizId = dto.QuizId,
            StartedAt = DateTime.UtcNow
        };

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
        attempt.CompletedAt = DateTime.UtcNow;
        attempt.Score = Math.Round((double)correct / dto.Answers.Count * 100, 2);

        await _attemptRepository.AddAttemptAsync(attempt);

        return new QuizResultDTO
        {
            Score = attempt.Score,
            TimeTaken = attempt.CompletedAt.Value - attempt.StartedAt
        };
    }

}
