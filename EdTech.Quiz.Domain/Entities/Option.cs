namespace EdTech.Quiz.Domain.Entities;

public class Option
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;
}
