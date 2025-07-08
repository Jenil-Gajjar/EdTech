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
        if (await _questionRepository.DoesQuestionAlreadyExists(dto.Text)) throw new Exception("Question already exists");

        if (!dto.Options.Any()) throw new Exception("Options are required");

        if (dto.CorrectOptionIndex < 0 || dto.CorrectOptionIndex >= dto.Options.Count) throw new Exception("Invalid correct option index");
        
        Question question = new()
        {
            Text = dto.Text.Trim()
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


    public async Task<List<QuestionDTO>> GetRandomQuestionsByQuizIdAsync(int QuizId, int Count)
    {

        List<QuestionDTO> result = (await _questionRepository.GetQuestionsByQuizIdAsync(QuizId)).Select(u => new QuestionDTO()
        {
            Id = u.Id,
            Text = u.Text,
            Options = u.Options.Select(x => new OptionDTO()
            {
                Id = x.Id,
                Text = x.Text
            }).ToList(),
            CorrectOption = u.Options.First(o => o.Id == u.CorrectOptionId).Text,
            CorrectOptionId = u.Options.First(o => o.Id == u.CorrectOptionId).Id
        }).OrderBy(x => Guid.NewGuid()).Take(Count).ToList();

        return result;
    }

    public async Task<List<QuestionDTO>> GetRandomQuestionsAsync(int Count)
    {
        List<QuestionDTO> result = (await _questionRepository.GetQuestionsAsync()).Select(u => new QuestionDTO()
        {
            Id = u.Id,
            Text = u.Text,
            Options = u.Options.Select(x => new OptionDTO()
            {
                Id = x.Id,
                Text = x.Text
            }).ToList(),
            CorrectOption = u.Options.First(o => o.Id == u.CorrectOptionId).Text,
            CorrectOptionId = u.Options.First(o => o.Id == u.CorrectOptionId).Id

        }).OrderBy(x => Guid.NewGuid()).Take(Count).ToList();

        return result;
    }


}
