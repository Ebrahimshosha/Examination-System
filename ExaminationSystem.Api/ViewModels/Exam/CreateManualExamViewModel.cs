namespace ExaminationSystem.Api.ViewModels.Exam;

public class CreateManualExamViewModel
{
    public DateTime StartDate { get; set; }

    public ICollection<int> QuestionsIDs { get; set; }

    public int InstructorId { get; set; }

    public int CourseId { get; set; }
    public string ExamStatus { get; set; }

}
