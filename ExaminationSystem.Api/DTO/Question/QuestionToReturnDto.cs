using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.DTO.Question;

public class QuestionToReturnDto
{
    public string Text { get; set; }
    public int Grade { get; set; }
    public List<string> Choices { get; set; } = new List<string>();
}
