
using EdTech.Quiz.Application.DTOs.Response;

namespace EdTech.Quiz.Application.DTOs.Request;
public class UpdateQuestionDTO
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public List<OptionDTO> Options { get; set; } = new();
    public int CorrectOptionId { get; set; }

}
