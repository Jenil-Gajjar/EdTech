using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : Controller
{

    private readonly IQuizService _quizService;
    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuizDTO dto)
    {
        var result = await _quizService.CreateQuizAsync(dto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _quizService.GetQuizByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _quizService.GetAllQuizzesAsync();
        return Ok(result);
    }
}
