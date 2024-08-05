namespace ExaminationSystem.Api.ViewModels.Exam;

public class CreateAutomaticExamViewModel
{
    public DateTime StartDate { get; set; }
    public int InstructorId { get; set; }
    public int CourseId { get; set; }
    public string ExamStatus { get; set; }
    public int NunmberOfQuestions { get; set; }

}
