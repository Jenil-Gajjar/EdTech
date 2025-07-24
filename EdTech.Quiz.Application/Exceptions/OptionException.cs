namespace EdTech.Quiz.Application.Exceptions;

public class OptionException : ApplicationException
{
    protected OptionException(string message, string ErrorCode) : base(message, ErrorCode) { }
    protected OptionException(string message, string ErrorCode, Exception innerException) : base(message, ErrorCode, innerException) { }

}

public class OptionInvalidIdsException : OptionException
{
    public OptionInvalidIdsException() : base("Invalid option ids.", "option_invalid_ids_exception") { }
}
public class OptionCorrectIdInvalidException : OptionException
{
    public OptionCorrectIdInvalidException() : base("Invalid correct option id.", "option_invalid_ids_exception") { }
}