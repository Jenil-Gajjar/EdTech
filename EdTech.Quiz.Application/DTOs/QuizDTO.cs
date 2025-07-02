namespace EdTech.Quiz.Application.DTOs;

public class QuizDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public double Score { get; set; }

    public List<QuestionDTO> Questions { get; set; } = new();
}
