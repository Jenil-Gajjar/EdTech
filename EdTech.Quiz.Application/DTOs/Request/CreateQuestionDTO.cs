namespace EdTech.Quiz.Application.DTOs.Request;
public class CreateQuestionDTO
{
    public string Text { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public int CorrectOptionIndex { get; set; }

}
