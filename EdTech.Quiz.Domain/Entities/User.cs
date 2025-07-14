namespace EdTech.Quiz.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email {get;set;} = string.Empty;
    public string Password {get;set;} = string.Empty;
    public ICollection<UserQuizAttempt> Attempts = new List<UserQuizAttempt>();
}