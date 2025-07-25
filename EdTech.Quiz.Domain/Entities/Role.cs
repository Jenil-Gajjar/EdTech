namespace EdTech.Quiz.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; } = new List<User>();

}
