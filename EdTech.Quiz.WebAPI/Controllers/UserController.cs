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
            bool res = await _userService.DeleteUserByIdAsync(id);
            if (res)
            {
                return StatusCode((int)HttpStatusCode.OK, new ResponseDTO()
                {
                    IsSuccess = true,
                    Message = "User deleted successfully."
                });
            }
            return BadRequest(new ResponseDTO()
            {
                Message = $"User with id {id} could not be deleted because it does not exist.",
                IsSuccess = false,
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
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]

    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateUserDTO dto)
    {

        if (!ModelState.IsValid) return BadRequest("Invalid Data");

        if (id != dto.Id) return BadRequest("Id in route does not match Id in body.");

        try
        {
            ResponseDTO responseDTO = await _userService.UpdateUserAsync(dto);

            if (responseDTO.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, responseDTO);

            else
                return StatusCode((int)HttpStatusCode.Conflict, responseDTO);

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
