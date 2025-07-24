using EdTech.Quiz.Application.Constants;
using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Application.Response.DTOs;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHistory(int id)
    {
        UserQuizHistoryDTO result = await _userService.GetUserQuizHistoryAsync(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        bool res = await _userService.DeleteUserByIdAsync(id);
        if (res)
        {
            return Ok(new ResponseDTO()
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

    [HttpPut]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateUserDTO dto)
    {

        if (id != dto.Id) return BadRequest("Id in route does not match Id in body.");

        ResponseDTO responseDTO = await _userService.UpdateUserAsync(dto);

        if (responseDTO.IsSuccess)
            return Ok(responseDTO);
        else
            return Conflict(responseDTO);

    }
}
