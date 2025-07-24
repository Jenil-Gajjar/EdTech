namespace EdTech.Quiz.Application.Exceptions;

public class QuestionException : ApplicationException
{
    protected QuestionException(string message, string ErrorCode) : base(message, ErrorCode) { }
    protected QuestionException(string message, string ErrorCode, Exception innerException) : base(message, ErrorCode, innerException) { }
}

public class QuestionNotFoundException : QuestionException
{
    public QuestionNotFoundException() : base("Question not found.", "question_not_found_exception") { }
}
public class QuestionAlreadyExistsException : QuestionException
{
    public QuestionAlreadyExistsException() : base("Question already exists.", "question_already_exists_exception") { }
}

public class QuestionInvalidIdsException : QuestionException
{
    public QuestionInvalidIdsException() : base("Invalid question ids.", "question_invalid_ids_exception") { }
}