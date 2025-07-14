using System.ComponentModel.DataAnnotations;

namespace EdTech.Quiz.Application.DTOs;

public class LoginDTO
{
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9_-]{3,20}$",
        ErrorMessage = "Username must be 3-20 characters long and can only contain letters, numbers, underscores, or hyphens.")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public string Password { get; set; } = string.Empty;
}
