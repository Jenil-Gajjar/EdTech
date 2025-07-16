using System.Net;
using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[AllowAnonymous]
public class AuthController : Controller
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] LoginDTO dto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid Data");
        }

        try
        {
            ResponseDTO responseDTO = await _authService.SignIn(dto);
            if (responseDTO.IsSuccess)
            {
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse<object>()
                {
                    Data = responseDTO.Data,
                    IsSuccess = true,
                    Message = responseDTO.Message,
                });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new ApiResponse<string>()
                {
                    Data = "SignIn failed.",
                    IsSuccess = false,
                    Message = responseDTO.Message,
                });
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<string>()
            {
                Data = "Something went wrong.",
                IsSuccess = false,
                Message = e.Message,
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] RegisterDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid Data");
        }
        try
        {
            ResponseDTO responseDTO = await _authService.SignUp(dto);
            if (responseDTO.IsSuccess)
            {
                return StatusCode((int)HttpStatusCode.Created, new ApiResponse<string>()
                {
                    IsSuccess = true,
                    Message = responseDTO.Message,
                });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Conflict, new ApiResponse<string>()
                {
                    Data = "Signup failed.",
                    IsSuccess = false,
                    Message = responseDTO.Message,
                });
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<string>()
            {
                Data = "Something went wrong.",
                IsSuccess = false,
                Message = e.Message,
            });
        }
    }
}
