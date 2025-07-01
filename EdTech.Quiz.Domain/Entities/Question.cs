namespace EdTech.Quiz.Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public ICollection<Option> Options { get; set; } = new List<Option>();
    public int CorrectOptionId { get; set; }
    // public Option? CorrectOption { get; set; }

    public List<QuizQuestion> QuizQuestions { get; set; } = null!;

}


