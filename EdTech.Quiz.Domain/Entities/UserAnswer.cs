namespace EdTech.Quiz.Domain.Entities;

public class UserAnswer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;
    public int SelectedOptionId { get; set; }
    public int UserQuizAttemptId { get; set; }
    public virtual UserQuizAttempt Attempt { get; set; } = null!;
}
