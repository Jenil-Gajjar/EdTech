namespace EdTech.Quiz.Domain.Entities;

public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
    public virtual ICollection<UserQuizAttempt> Attempts { get; set; } = new List<UserQuizAttempt>();
}
