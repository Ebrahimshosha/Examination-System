namespace ExaminationSystem.Api.DTO.Exam;

public class ExamManualDto
{
    public DateTime StartDate { get; set; }
    public int InstructorId { get; set; }
    public int CourseId { get; set; }
    public string ExamStatus { get; set; }
    public ICollection<int> QuestionsIDs { get; set; }

}
