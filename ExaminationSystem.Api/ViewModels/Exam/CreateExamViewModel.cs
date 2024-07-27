namespace ExaminationSystem.Api.ViewModels.Exam;

public class CreateExamViewModel
{
    public DateTime StartDate { get; set; }

    public ICollection<int> QuestionsIDs { get; set; }

    public int TotalGrade { get; set; }

    public int InstructorId { get; set; }

    public int CourseId { get; set; }
}
