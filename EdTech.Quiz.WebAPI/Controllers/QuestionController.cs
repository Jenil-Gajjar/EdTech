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
            if (!dto.Options.Any()) return BadRequest("Options are required.");

            if (dto.Options.Count != 4) return BadRequest("The number of options must be 4.");

            if (dto.CorrectOptionIndex < 0 || dto.CorrectOptionIndex >= dto.Options.Count) return BadRequest("Invalid correct option index.");

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

    [HttpGet("{id}")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]

    public async Task<IActionResult> Get(int id)
    {
        try
        {
            QuestionDTO? question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return BadRequest(new ResponseDTO()
                {
                    IsSuccess = false,
                    Message = "Invalid Question Id."
                });

            return Ok(new ResponseDTO()
            {
                Data = question,
                IsSuccess = true,
                Message = "Data fetched successfully."
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
            PaginatedResult<QuestionDTO> result = _questionService.GetRandomQuestions(quizId, dto);

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
                return StatusCode((int)HttpStatusCode.OK, new ResponseDTO()
                {
                    IsSuccess = true,
                    Message = "Question deleted successfully."
                });
            }
            return BadRequest(new ResponseDTO()
            {
                IsSuccess = false,
                Message = $"Question with id {id} could not be deleted because it does not exist.",
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

    [HttpPut]
    [Authorize(Roles = UserRoles.Admin)]

    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateQuestionDTO dto)
    {
        if (id != dto.Id)
            return BadRequest("Id in route does not match Id in body.");

        try
        {
            bool res = await _questionService.UpdateQuestionAsync(dto);
            if (res)
            {
                return StatusCode((int)HttpStatusCode.OK, new ResponseDTO()
                {
                    IsSuccess = true,
                    Message = "Question updated successfully."
                });
            }
            return BadRequest(new ResponseDTO()
            {
                IsSuccess = false,
                Message = $"Question with id {id} could not be updated because it does not exist.",
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