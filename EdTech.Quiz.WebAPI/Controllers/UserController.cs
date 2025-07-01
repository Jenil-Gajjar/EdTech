using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

public class UserController : Controller
{
    private readonly IAttemptService _attemptService;

    public UserController(IAttemptService attemptService)
    {
        _attemptService = attemptService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserHistory(int id)
    {
        var result = await _attemptService.GetUserQuizHistoryAsync(id);
        return Ok(result);
    }
}
