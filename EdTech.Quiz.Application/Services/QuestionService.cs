using EdTech.Quiz.Application.DTOs;
using EdTech.Quiz.Application.Interface.Repositories;
using EdTech.Quiz.Application.Interface.Services;
using EdTech.Quiz.Domain.Entities;

namespace EdTech.Quiz.Application.Services;

public class QuestionService : IQuestionService

{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    public async Task<int> CreateQuestionAsync(CreateQuestionDTO dto)
    {
        Question question = new()
        {
            Text = dto.Text
        };
        foreach (string optionText in dto.Options)
        {
            question.Options.Add(new() { Text = optionText });
        }
        await _questionRepository.AddQuestionAsync(question);
        await _questionRepository.SaveChangesAsync();

        question.CorrectOptionId = ((List<Option>)question.Options)[dto.CorrectOptionIndex].Id;

        await _questionRepository.SaveChangesAsync();

        return question.Id;

    }


    public async Task<List<Question>> GetRandomQuestionsAsync(int QuizId, int Count)
    {
        return await _questionRepository.GetRandomQuestionsAsync(QuizId, Count);
    }

}
