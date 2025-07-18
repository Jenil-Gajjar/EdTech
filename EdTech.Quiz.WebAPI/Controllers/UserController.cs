using System.Net;
using EdTech.Quiz.Application.Constants;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = UserRoles.User)]
    public async Task<IActionResult> GetUserHistory(int id)
    {
        try
        {
            UserQuizHistoryDTO? result = await _userService.GetUserQuizHistoryAsync(id);
            return Ok(result);
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
