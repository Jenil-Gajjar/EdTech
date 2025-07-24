namespace EdTech.Quiz.Application.DTOs.Request;
public class CreateQuizDTO
{
    public string Title { get; set; } = string.Empty;
    public List<int> QuestionIds { get; set; } = new();
}