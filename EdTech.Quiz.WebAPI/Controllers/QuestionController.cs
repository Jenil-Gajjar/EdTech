using System.Net;
using EdTech.Quiz.Application.Constants;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Helpers;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/questions")]
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
                Message = "Question created successfully."
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

    [HttpGet("random")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]

    public IActionResult GetRandomQuestions([FromQuery] int? quizId, [FromQuery] PaginationDTO dto)
    {
        try
        {
            PaginatedResult<QuestionDTO> result;

            if (quizId.HasValue)
            {
                result = _questionService.GetRandomQuestionsByQuizId(quizId.Value, dto);
            }
            else
            {
                result = _questionService.GetRandomQuestions(dto);
            }

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

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Admin)]

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            bool res = await _questionService.DeleteQuestionByIdAsync(id);
            if (res)
            {
                return Ok(new ResponseDTO()
                {
                    Data = $"Question with id {id} deleted sucessfully.",
                    IsSuccess = true,
                    Message = "Question deleted successfully."
                });
            }
            return BadRequest(new ResponseDTO()
            {
                Data = $"Question with id {id} could not be deleted because it does not exist.",
                IsSuccess = false,
                Message = "An error occurred while processing request.",
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