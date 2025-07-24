namespace EdTech.Quiz.Application.Exceptions;

public class UserException : ApplicationException
{
    protected UserException(string message, string ErrorCode) : base(message, ErrorCode) { }
    protected UserException(string message, string ErrorCode, Exception innerException) : base(message, ErrorCode, innerException) { }

}

public class UserAlreadyAttemptedQuizException : UserException
{
    public UserAlreadyAttemptedQuizException() : base("User has already attempted this quiz.", "user_already_attempted_quiz_exception") { }
}


public class UserNotFoundException : UserException
{
    public UserNotFoundException() : base("User not found.", "user_not_found_exception") { }
}
