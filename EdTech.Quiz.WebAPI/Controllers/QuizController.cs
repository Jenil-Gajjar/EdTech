using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class QuizController : Controller
{

    private readonly IQuizService _quizService;
    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuizDTO dto)
    {
        try
        {

            int result = await _quizService.CreateQuizAsync(dto);

            return Ok(new ApiResponse<object>()
            {
                Data = result,
                IsSuccess = true,
                Message = "Quiz Created Successfully!"
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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            QuizDTO result = await _quizService.GetQuizByIdAsync(id);
            return Ok(new ApiResponse<object>()
            {
                Data = result,
                IsSuccess = true,
                Message = "Data Fetched Successfully!"
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            List<QuizDTO> result = await _quizService.GetAllQuizzesAsync();
            return Ok(new ApiResponse<object>()
            {
                Data = result,
                IsSuccess = true,
                Message = $"Total Records: {result.Count}"

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
