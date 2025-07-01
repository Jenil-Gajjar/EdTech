using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdTech.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : Controller
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuestionDTO dto)
    {
        var result = await _questionService.CreateQuestionAsync(dto);
        return Ok(result);
    }

    [HttpGet("random")]

    public async Task<IActionResult> GetRandom(int QuizId, int Count)
    {
        var result = await _questionService.GetRandomQuestionsAsync(QuizId, Count);
        return Ok(result);
    }
}