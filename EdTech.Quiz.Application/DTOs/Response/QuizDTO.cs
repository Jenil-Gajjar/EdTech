namespace EdTech.Quiz.Application.DTOs.Response;

public class QuizDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<QuestionDTO>? Questions { get; set; } 
}
