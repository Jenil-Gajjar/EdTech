namespace EdTech.Quiz.Application.Exceptions;

public class AuthenticationException : ApplicationException
{
    protected AuthenticationException(string message, string ErrorCode) : base(message, ErrorCode) { }
    protected AuthenticationException(string message, string ErrorCode, Exception innerException) : base(message, ErrorCode, innerException) { }
}
public class AuthenticationFailedException : AuthenticationException
{
    public AuthenticationFailedException() : base("Invalid username or password.", "authentication_failed_exception") { }

}

public class EmailAlreadyExistsException : AuthenticationException
{
    public EmailAlreadyExistsException() : base("Email already exists.", "email_already_exists_exception") { }
}
public class UsernameAlreadyExistsException : AuthenticationException
{
    public UsernameAlreadyExistsException() : base("Username already exists.", "username_already_exists_exception") { }
}

public class RecordsNotFoundException : AuthenticationException
{
    public RecordsNotFoundException() : base("No records found.", "records_not_found_exception") { }
}