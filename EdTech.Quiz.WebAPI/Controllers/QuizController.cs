using System.Net;
using EdTech.Quiz.Application.Constants;
using EdTech.Quiz.Application.DTOs;
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
    public async Task<IActionResult> Create([FromBody] CreateQuizDTO dto)
    {
        try
        {
            int result = await _quizService.CreateQuizAsync(dto);

            return StatusCode((int)HttpStatusCode.Created, new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = "Quiz created successfully."
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
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            QuizDTO? quiz = await _quizService.GetQuizByIdAsync(id);

            if (quiz == null)
                return BadRequest(new ResponseDTO()
                {
                    IsSuccess = false,
                    Message = "Invalid Quiz Id."
                });

            return Ok(new ResponseDTO()
            {
                Data = quiz,
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

    [HttpGet]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]

    public IActionResult GetAll([FromQuery] PaginationDTO dto)
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

    [HttpPost("{quizId}/start-attempt")]
    [Authorize(Roles = UserRoles.User)]

    public async Task<IActionResult> StartAttempt(int quizId, [FromBody] StartQuizAttemptDTO dto)
    {

        if (quizId != dto.QuizId)
            return BadRequest("Quiz ID in route does not match Quiz ID in body.");

        try
        {

            int result = await _attemptService.StartAttemptAsync(dto);
            return Ok(new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = "Quiz started successfully."
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

    [HttpPost("{quizId}/submit-attempt")]
    [Authorize(Roles = UserRoles.User)]

    public async Task<IActionResult> SubmitAttempt(int quizId, UserQuizAttemptDTO dto)
    {

        if (quizId != dto.QuizId)
            return BadRequest("Quiz ID in route does not match Quiz ID in body.");
        try
        {
            QuizResultDTO result = await _attemptService.SubmitAttemptAsync(dto);
            return Ok(new ResponseDTO()
            {
                Data = result,
                IsSuccess = true,
                Message = "Attempt saved successfully."
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
            bool res = await _quizService.DeleteQuizByIdAsync(id);
            if (res)
            {
                return StatusCode((int)HttpStatusCode.OK, new ResponseDTO()
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

    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateQuizDTO dto)
    {
        if (id != dto.Id)
            return BadRequest("Id in route does not match Id in body.");
        try
        {
            bool res = await _quizService.UpdateQuizAsync(dto);
            if (res)
            {
                return StatusCode((int)HttpStatusCode.OK, new ResponseDTO()
                {
                    IsSuccess = true,
                    Message = "Quiz updated successfully."
                });
            }
            return BadRequest(new ResponseDTO()
            {
                IsSuccess = false,
                Message = $"Question with id {id} could not be update because it does not exist."
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
