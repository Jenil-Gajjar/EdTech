namespace EdTech.Quiz.Domain.Entities;

public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
    public ICollection<UserQuizAttempt> Attempts { get; set; } = new List<UserQuizAttempt>();
}
