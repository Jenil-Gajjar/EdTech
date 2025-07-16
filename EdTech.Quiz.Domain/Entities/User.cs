namespace EdTech.Quiz.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
    public virtual ICollection<UserQuizAttempt> Attempts { get; set; } = new List<UserQuizAttempt>();
}