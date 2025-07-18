using System.Net;
using EdTech.Quiz.Application.Constants;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Helpers;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class QuizController : Controller
{
    private readonly IQuizService _quizService;
    private readonly IAttemptService _attemptService;
    public QuizController(IQuizService quizService, IAttemptService attemptService)
    {
        _quizService = quizService;
        _attemptService = attemptService;
    }
    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateQuizDTO dto)
    {
        try
        {
            int result = await _quizService.CreateQuizAsync(dto);

            return StatusCode((int)HttpStatusCode.Created, new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = "Quiz Created Successfully!"
            });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseDTO()
            {
                IsSuccess = false,
                Data = e.Message,
                Message = "An error occurred while processing request.",
            });
        }
    }

    [HttpGet("{id}")]

    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    public async Task<IActionResult> GetQuiz(int id)
    {
        try
        {
            QuizDTO result = await _quizService.GetQuizByIdAsync(id);
            return Ok(new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = "Data Fetched Successfully!"
            });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseDTO()
            {
                Data = e.Message,
                IsSuccess = false,
                Message = "An error occurred while processing request.",
            });
        }
    }

    [HttpGet]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]

    public IActionResult GetQuizzes([FromQuery] PaginationDTO dto)
    {
        try
        {
            PaginatedResult<QuizDTO> result = _quizService.GetAllQuizzes(dto);
            return Ok(new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = $"Data fetched successfully."
            });
        }
        catch (Exception e)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseDTO()
            {
                Data = e.Message,
                IsSuccess = false,
                Message = "An error occurred while processing request.",
            });
        }
    }


    [HttpPost]
    [Authorize(Roles = UserRoles.User)]

    public async Task<IActionResult> StartAttempt([FromBody] StartQuizAttemptDTO dto)
    {
        try
        {

            int result = await _attemptService.StartAttemptAsync(dto);
            return Ok(new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = "Quiz Started Successfully!"
            });
        }
        catch (Exception e)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseDTO()
            {
                Data = e.Message,
                IsSuccess = false,
                Message = "An error occurred while processing request.",
            });
        }

    }

    [HttpPost]
    [Authorize(Roles = UserRoles.User)]

    public async Task<IActionResult> SubmitAttempt(UserQuizAttemptDTO dto)
    {
        try
        {
            QuizResultDTO result = await _attemptService.SubmitAttemptAsync(dto);
            return Ok(new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = "Attempt Saved Successfully!"
            });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseDTO()
            {
                Data = e.Message,
                IsSuccess = false,
                Message = "An error occurred while processing request.",
            });
        }
    }

}
