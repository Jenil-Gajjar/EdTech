namespace EdTech.Quiz.Application.DTOs;

public class QuizResultDTO
{
    public int AttemptId { get; set; }
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Score { get; set; }
    public TimeSpan TimeTaken { get; set; }

}
