using EdTech.Quiz.Application.Constants;
using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Create([FromBody] CreateQuestionDTO dto)
    {

        if (!dto.Options.Any()) return BadRequest("Options are required.");

        if (dto.Options.Count != 4) return BadRequest("The number of options must be 4.");

        if (dto.CorrectOptionIndex < 0 || dto.CorrectOptionIndex >= dto.Options.Count) return BadRequest("Invalid correct option index.");

        int result = await _questionService.CreateQuestionAsync(dto);

        return StatusCode(StatusCodes.Status201Created, new ResponseDTO()
        {
            Data = result,
            IsSuccess = true,
            Message = "Question created successfully."
        });
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {

        QuestionDTO question = await _questionService.GetQuestionByIdAsync(id);

        return Ok(new ResponseDTO()
        {
            Data = question,
            IsSuccess = true,
            Message = "Data fetched successfully."
        });

    }

    [HttpGet("random")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetRandomQuestions([FromQuery] int? quizId, [FromQuery] PaginationDTO dto)
    {

        PaginatedResult<QuestionDTO> result = _questionService.GetRandomQuestions(quizId, dto);

        return Ok(new ResponseDTO()
        {
            Data = result,
            IsSuccess = true,
            Message = $"Data fetched successfully."
        });

    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {

        bool res = await _questionService.DeleteQuestionByIdAsync(id);
        if (res)
        {
            return Ok(new ResponseDTO()
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

    [HttpPut]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateQuestionDTO dto)
    {
        if (id != dto.Id)
            return BadRequest("Id in route does not match Id in body.");

        bool res = await _questionService.UpdateQuestionAsync(dto);
        if (res)
        {
            return Ok(new ResponseDTO()
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
}