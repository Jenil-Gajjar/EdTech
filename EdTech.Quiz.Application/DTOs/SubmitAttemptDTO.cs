namespace EdTech.Quiz.Application.DTOs;

public class  SubmitAttemptDTO
{
    public int UserId { get; set; }
    public int QuizId { get; set; }

    public List<UserAnswerDTO> Answers = new();

}
