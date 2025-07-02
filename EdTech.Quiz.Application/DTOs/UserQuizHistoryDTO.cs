namespace EdTech.Quiz.Application.DTOs;

public class UserQuizHistoryDTO
{
    public string Name { get; set; } = string.Empty;

    public List<QuizDTO> Quizzes { get; set; } = new();
}
