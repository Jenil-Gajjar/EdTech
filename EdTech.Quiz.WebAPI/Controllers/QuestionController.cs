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
public class QuestionController : Controller
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateQuestionDTO dto)
    {
        try
        {

            int result = await _questionService.CreateQuestionAsync(dto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = "Question Created Successfully!"
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

    [HttpGet("{QuizId}")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]

    public IActionResult GetRandomQuestions(int QuizId, [FromQuery] PaginationDTO dto)
    {
        try
        {
            PaginatedResult<QuestionDTO> result = _questionService.GetRandomQuestionsByQuizId(QuizId, dto);
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
    [HttpGet]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]

    public IActionResult GetRandomQuestions([FromQuery] PaginationDTO dto)
    {
        try
        {
            PaginatedResult<QuestionDTO> result = _questionService.GetRandomQuestions(dto);
            return Ok(new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = $"Data fetched successfully."
            }); ;
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