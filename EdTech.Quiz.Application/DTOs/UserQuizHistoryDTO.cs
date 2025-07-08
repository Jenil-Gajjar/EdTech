namespace EdTech.Quiz.Application.DTOs;

public class UserQuizHistoryDTO
{
    public class QuizDetails
    {
        public string Title { get; set; } = string.Empty;

        public double Score { get; set; }

        public TimeSpan TimeTaken { get; set; }
    }
    public string Name { get; set; } = string.Empty;

    public List<QuizDetails> Quizzes { get; set; } = new();
}
