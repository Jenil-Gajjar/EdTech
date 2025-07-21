using System.Net;
using EdTech.Quiz.Application.Constants;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}/history")]
    [Authorize(Roles = UserRoles.User)]
    public async Task<IActionResult> GetHistory(int id)
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


    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var res = await _userService.DeleteUserByIdAsync(id);
            if (res)
            {
                return Ok(new ResponseDTO()
                {
                    Data = $"User with id {id} deleted sucessfully.",
                    IsSuccess = true,
                    Message = "User deleted successfully."
                });
            }
            return BadRequest(new ResponseDTO()
            {
                Data = $"User with id {id} could not be deleted because it does not exist.",
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
