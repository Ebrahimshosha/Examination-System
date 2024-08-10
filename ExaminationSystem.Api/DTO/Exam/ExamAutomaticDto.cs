namespace ExaminationSystem.Api.DTO.Exam;

public class ExamAutomaticDto
{
    public DateTime StartDate { get; set; }
    public int InstructorId { get; set; }
    public int CourseId { get; set; }
    public string ExamStatus { get; set; }
    public int NunmberOfQuestions { get; set; }
}
