using EdTech.Quiz.Application.DTOs.Request;
using EdTech.Quiz.Application.DTOs.Response;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController : Controller
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> SignIn([FromBody] LoginDTO dto)
    {
        ResponseDTO responseDTO = await _authService.SignIn(dto);
        if (responseDTO.IsSuccess)
            return Ok(responseDTO);
        else
            return Unauthorized(responseDTO);

    }

    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignUp([FromBody] RegisterDTO dto)
    {

        ResponseDTO responseDTO = await _authService.SignUp(dto);
        if (responseDTO.IsSuccess)
            return StatusCode(StatusCodes.Status201Created, responseDTO);
        else
            return Conflict(responseDTO);

    }
}
