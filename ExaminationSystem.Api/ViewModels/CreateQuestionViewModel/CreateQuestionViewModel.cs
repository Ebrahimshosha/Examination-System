using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.ViewModels.CreateQuestionViewModel;

public class CreateQuestionViewModel
{
    public string Text { get; set; }
    public int Grade { get; set; }
    public List<string> Choices { get; set; } = new List<string>();
    public string RightAnswer { get; set; }
    public int ExamId { get; set; }
}
