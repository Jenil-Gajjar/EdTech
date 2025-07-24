namespace EdTech.Quiz.Application.DTOs.Request;
public class UpdateQuizDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<int> QuestionIds { get; set; } = new();
}
