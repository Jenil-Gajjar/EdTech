using System.ComponentModel.DataAnnotations;

namespace EdTech.Quiz.Application.DTOs;

public class UpdateUserDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z]{3}[a-zA-Z0-9_-]{5,17}$",
        ErrorMessage = "Username must be 8-20 characters long and can only contain letters, numbers, underscores, or hyphens.")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.[a-zA-Z]{2,}([-.]\w+)*$", ErrorMessage = "Invalid email address.")]

    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public string Password { get; set; } = string.Empty;
}
