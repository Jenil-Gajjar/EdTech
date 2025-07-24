namespace EdTech.Quiz.Application.Exceptions;

public abstract class ApplicationException : Exception
{
    public string ErrorCode { get; }

    protected ApplicationException(string message, string ErrorCode) : base(message)
    {
        this.ErrorCode = ErrorCode;
    }
    protected ApplicationException(string message, string ErrorCode, Exception innerException) : base(message, innerException)
    {
        this.ErrorCode = ErrorCode;
    }
}
