using EdTech.Quiz.Application.Constants;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Helpers;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/quizzes")]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateQuizDTO dto)
    {

        int result = await _quizService.CreateQuizAsync(dto);

        return StatusCode(StatusCodes.Status201Created, new ResponseDTO()
        {
            Data = result,
            IsSuccess = true,
            Message = "Quiz created successfully."
        });

    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        QuizDTO quiz = await _quizService.GetQuizByIdAsync(id);

        return Ok(new ResponseDTO()
        {
            Data = quiz,
            IsSuccess = true,
            Message = "Data fetched successfully."
        });

    }

    [HttpGet]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll([FromQuery] PaginationDTO dto)
    {

        PaginatedResult<QuizDTO> result = _quizService.GetAllQuizzes(dto);
        return Ok(new ResponseDTO()
        {
            Data = result,
            IsSuccess = true,
            Message = $"Data fetched successfully."
        });

    }

    [HttpPost("{quizId}/start-attempt")]
    [Authorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> StartAttempt(int quizId, [FromBody] StartQuizAttemptDTO dto)
    {

        if (quizId != dto.QuizId)
            return BadRequest("Quiz ID in route does not match Quiz ID in body.");

        int result = await _attemptService.StartAttemptAsync(dto);
        return Ok(new ResponseDTO()
        {
            Data = result,
            IsSuccess = true,
            Message = "Quiz started successfully."
        });


    }

    [HttpPost("{quizId}/submit-attempt")]
    [Authorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> SubmitAttempt(int quizId, [FromBody] UserQuizAttemptDTO dto)
    {

        if (quizId != dto.QuizId)
            return BadRequest("Quiz ID in route does not match Quiz ID in body.");

        QuizResultDTO result = await _attemptService.SubmitAttemptAsync(dto);
        return Ok(new ResponseDTO()
        {
            Data = result,
            IsSuccess = true,
            Message = "Attempt saved successfully."
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

        bool res = await _quizService.DeleteQuizByIdAsync(id);
        if (res)
        {
            return Ok(new ResponseDTO()
            {
                IsSuccess = true,
                Message = "Quiz deleted successfully."
            });
        }
        return BadRequest(new ResponseDTO()
        {
            IsSuccess = false,
            Message = $"Quiz with id {id} could not be deleted because it does not exist."
        });

    }
    [HttpPut]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateQuizDTO dto)
    {
        if (id != dto.Id)
            return BadRequest("Id in route does not match Id in body.");

        bool res = await _quizService.UpdateQuizAsync(dto);
        if (res)
            return Ok(new ResponseDTO()
            {
                IsSuccess = true,
                Message = "Quiz updated successfully."
            });
        else
            return BadRequest(new ResponseDTO()
            {
                IsSuccess = false,
                Message = $"Question with id {id} could not be update because it does not exist."
            });

    }

}
