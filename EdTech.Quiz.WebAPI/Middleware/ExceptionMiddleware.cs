using System.Net;
using System.Text.Json;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Exceptions;

namespace EdTech.Quiz.WebAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code;
        string message;

        switch (exception)
        {
            case ArgumentNullException:
            case ArgumentException:
            case FormatException:
                code = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            case AuthenticationFailedException:
                code = HttpStatusCode.Unauthorized;
                message = exception.Message;
                break;

            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                message = "Access is denied.";
                break;

            case TimeoutException:
                code = HttpStatusCode.RequestTimeout;
                message = "The request timed out.";
                break;

            case NotImplementedException:
                code = HttpStatusCode.NotImplemented;
                message = "The functionality is not implemented.";
                break;


            case QuizNotFoundException:
            case QuestionNotFoundException:
            case AttemptsNotFoundException:
            case RecordsNotFoundException:
            case UserNotFoundException:
                code = HttpStatusCode.NotFound;
                message = exception.Message;
                break;

            case UserAlreadyAttemptedQuizException:
            case QuestionAlreadyExistsException:
            case QuizAlreadyExistsException:
            case EmailAlreadyExistsException:
            case UsernameAlreadyExistsException:
                code = HttpStatusCode.Conflict;
                message = exception.Message;
                break;

            case QuizNotStartedException:
            case QuizAlreadyCompletedException:
            case QuizAttemptTimeLimitException:
            case QuizInvalidIdException:
            case QuizException:

            case QuestionInvalidIdsException:
            case QuestionException:

            case OptionInvalidIdsException:
            case OptionCorrectIdInvalidException:
            case OptionException:

            case AttemptException:
            case UserException:
            case AuthenticationException:
                code = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            default:
                code = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred. Please try again later.";
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        ResponseDTO errorResponse = new()
        {
            Data = null,
            IsSuccess = false,
            Message = message
        };

        string json = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(json);

        _logger.LogError("Status Code: {code}, Message: {message},Exception Details: {Message}", code, message, exception.Message);
    }

}
