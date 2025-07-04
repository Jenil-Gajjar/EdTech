using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly IAttemptService _attemptService;
    private readonly IUserService _userService;

    public UserController(IAttemptService attemptService, IUserService userService)
    {
        _attemptService = attemptService;
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserHistory(int id)
    {
        try
        {
            UserQuizHistoryDTO? result = await _attemptService.GetUserQuizHistoryAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<string>()
            {
                IsSuccess = false,
                Data = e.Message,
                Message = "An error occurred while processing request.",
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
    {
        try
        {
            int result = await _userService.CreateUserAsync(dto);
            return Ok(new ApiResponse<object>()
            {
                IsSuccess = true,
                Data = result,
                Message = "User Created Successfully!"
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<string>()
            {
                IsSuccess = false,
                Data = e.Message,
                Message = "An error occurred while processing request.",
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> StartAttemptAsync([FromBody] StartQuizAttemptDTO dto)
    {
        try
        {

            int result = await _attemptService.StartAttemptAsync(dto);
            return Ok(new ApiResponse<object>()
            {
                IsSuccess = true,
                Data = result,
                Message = "Quiz Started Successfully!"
            });
        }
        catch (Exception e)
        {

            return StatusCode(500, new ApiResponse<string>()
            {
                IsSuccess = false,
                Data = e.Message,
                Message = "An error occurred while processing request.",
            });
        }

    }

    [HttpPost]
    public async Task<IActionResult> SubmitAttemptAsync(UserQuizAttemptDTO dto)
    {
        try
        {
            QuizResultDTO result = await _attemptService.SubmitAttemptAsync(dto);
            return Ok(new ApiResponse<object>()
            {
                IsSuccess = true,
                Data = result,
                Message = "Attempt Saved Successfully!"
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<string>()
            {
                IsSuccess = false,
                Data = e.Message,
                Message = "An error occurred while processing request.",
            });
        }
    }
}
