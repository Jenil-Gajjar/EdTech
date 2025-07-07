using System.Net;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class QuestionController : Controller
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuestionDTO dto)
    {
        try
        {

            int result = await _questionService.CreateQuestionAsync(dto);
            return StatusCode((int)HttpStatusCode.Created, new ApiResponse<object>()
            {
                IsSuccess = true,
                Data = result,
                Message = "Question Created Successfully!"
            });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<string>()
            {
                IsSuccess = false,
                Data = e.Message,
                Message = "An error occurred while processing request.",
            });
        }
    }

    [HttpGet("{QuizId}/{Count}")]

    public async Task<IActionResult> GetRandomQuestions(int QuizId, int Count)
    {

        try
        {
            List<QuestionDTO> result = await _questionService.GetRandomQuestionsByQuizIdAsync(QuizId, Count);
            return Ok(new ApiResponse<object>()
            {
                IsSuccess = true,
                Data = result,
                Message = $"Total Records: {result.Count}"
            });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<string>()
            {
                IsSuccess = false,
                Data = e.Message,
                Message = "An error occurred while processing request.",
            });
        }

    }
    [HttpGet("{Count}")]

    public async Task<IActionResult> GetRandomQuestions(int Count)
    {
        try
        {
            List<QuestionDTO> result = await _questionService.GetRandomQuestionsAsync(Count);
            return Ok(new ApiResponse<object>()
            {
                IsSuccess = true,
                Data = result,
                Message = $"Total Records: {result.Count}"
            }); ;
        }
        catch (Exception e)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<string>()
            {
                IsSuccess = false,
                Data = e.Message,
                Message = "An error occurred while processing request.",
            });
        }
    }




}