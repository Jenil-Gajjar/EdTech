using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Services;
using EdTech.Quiz.Domain.Entities;


namespace EdTech.Quiz.UnitTests;

public class QuizServiceTests
{
    [Fact]
    public void ScoreTest()
    {
        UserQuizAttemptDTO dto = new()
        {
            QuizId = 3,
            UserId = 4,
            Answers = new()
            {
                new(){QuestionId = 2,SelectedOptionId= 8},
                new(){QuestionId = 3,SelectedOptionId= 11}
            }
        };

        UserQuizAttempt? attempt = new();
        List<Question>? questions = new()
        {
            new(){Id=2,CorrectOptionId=8},
            new(){Id=3,CorrectOptionId = 11}
        };

        double result = AttemptService.CalculateScore(attempt, dto, questions);

        Assert.Equal(100, result);
    }
}

