namespace EdTech.Quiz.Application.Exceptions;

public class QuizException : ApplicationException
{
    protected QuizException(string message, string ErrorCode) : base(message, ErrorCode) { }
    protected QuizException(string message, string ErrorCode, Exception innerException) : base(message, ErrorCode, innerException) { }
}

public class QuizAlreadyExistsException : QuizException
{
    public QuizAlreadyExistsException() : base("Quiz already exists.", "quiz_already_exists_exception") { }
}
public class QuizNotFoundException : QuizException
{
    public QuizNotFoundException() : base("Quiz not found.", "quiz_not_found_exception") { }
}

public class QuizInvalidIdException : QuizException
{
    public QuizInvalidIdException() : base("Invalid quiz id.", "invalid_quizod_exception") { }
}
public class QuizNotStartedException : QuizException
{
    public QuizNotStartedException() : base("Please start quiz first.", "quiz_not_started_exception") { }
}

public class QuizAlreadyCompletedException : QuizException
{
    public QuizAlreadyCompletedException() : base("You have already completed this quiz.", "quiz_already_completed_exception") { }
}

public class QuizAttemptTimeLimitException : QuizException
{
    public QuizAttemptTimeLimitException() : base("Quiz attempt exceeded 30-minute time limit.", "quiz_attempt_time_limit_exception") { }
}