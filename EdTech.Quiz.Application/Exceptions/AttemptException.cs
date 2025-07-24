namespace EdTech.Quiz.Application.Exceptions;

public class AttemptException : ApplicationException
{
    protected AttemptException(string message, string ErrorCode) : base(message, ErrorCode) { }
    protected AttemptException(string message, string ErrorCode, Exception innerException) : base(message, ErrorCode, innerException) { }

}

public class AttemptsNotFoundException : AttemptException
{
    public AttemptsNotFoundException() : base("No attempts found.", "attempts_not_found_exception") { }

}