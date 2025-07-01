namespace EdTech.Quiz.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<UserQuizAttempt> Attempts = new List<UserQuizAttempt>();
}