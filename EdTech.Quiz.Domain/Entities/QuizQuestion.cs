namespace EdTech.Quiz.Domain.Entities;

public class QuizQuestion
{
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

}

