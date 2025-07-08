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

        var attempt = new UserQuizAttempt();
        var questions = new List<Question>
        {
            new(){Id=2,CorrectOptionId=8},
            new(){Id=3,CorrectOptionId = 11}
        };

        var result = AttemptService.CalculateScore(attempt, dto, questions);

        Assert.Equal(100, result);
    }
}

