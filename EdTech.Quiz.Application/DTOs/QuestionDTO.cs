namespace EdTech.Quiz.Application.DTOs;

public class QuestionDTO
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public List<string> Options { get; set; } = new();
    public string CorrectOption { get; set; } = string.Empty;

    public int CorrectOptionId { get; set; }

}
