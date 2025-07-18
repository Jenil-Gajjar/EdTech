namespace EdTech.Quiz.Application.DTOs;

public class QuestionDTO
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public List<OptionDTO>? Options { get; set; }
    public string CorrectOption { get; set; } = string.Empty;
    public int CorrectOptionId { get; set; }

}
