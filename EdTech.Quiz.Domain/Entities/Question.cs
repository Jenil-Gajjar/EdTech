namespace EdTech.Quiz.Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();
    public int CorrectOptionId { get; set; }

    public virtual List<QuizQuestion> QuizQuestions { get; set; } = null!;

}


