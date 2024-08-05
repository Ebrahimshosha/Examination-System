using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.DTO.Question;

public class CreateQuestionDto
{
    public string Text { get; set; }
    public int Grade { get; set; }
    public List<string> Choices { get; set; } = new List<string>();
    public string RightAnswer { get; set; }
    public int ExamId { get; set; }
    public string Status { get; set; }

}
