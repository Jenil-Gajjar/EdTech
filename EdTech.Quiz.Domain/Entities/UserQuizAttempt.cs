namespace EdTech.Quiz.Domain.Entities;

public class UserQuizAttempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public double Score { get; set; }
    public ICollection<UserAnswer> Answers = new List<UserAnswer>();
}
