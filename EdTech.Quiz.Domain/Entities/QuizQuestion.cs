namespace EdTech.Quiz.Domain.Entities;

public class QuizQuestion
{
    public int QuizId { get; set; }
    public virtual Quiz Quiz { get; set; } = null!;
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;

}

