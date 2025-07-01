namespace EdTech.Quiz.Application.DTOs;

public class UserQuizAttemptDTO
{
    public int QuizId { get; set; }
    public string QuizTitle { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;

    public int Score { get; set; }
    public TimeSpan TimeTaken { get; set; }

}
