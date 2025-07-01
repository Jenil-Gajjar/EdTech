using System.ComponentModel.DataAnnotations;

namespace EdTech.Quiz.Application.DTOs;

public class CreateQuestionDTO
{
    public string Text { get; set; } = string.Empty;
    [Required]
    public List<string> Options = new();
    public int CorrectOptionIndex { get; set; }

}
