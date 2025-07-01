namespace EdTech.Quiz.Application.DTOs;

public class QuizResultDTO
{
    public int AttemptId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    public TimeSpan TimeTaken { get; set; }

}
