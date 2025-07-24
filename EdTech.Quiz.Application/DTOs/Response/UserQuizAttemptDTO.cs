using EdTech.Quiz.Application.DTOs.Response;

namespace EdTech.Quiz.Application.DTOs;
public class UserQuizAttemptDTO
{
    public int QuizId { get; set; }
    public int UserId { get; set; }
    public List<UserAnswerDTO> Answers { get; set; } = new();

}
