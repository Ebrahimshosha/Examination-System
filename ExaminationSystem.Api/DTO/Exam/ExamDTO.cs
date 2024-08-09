namespace ExaminationSystem.Api.DTO.Exam;

public class ExamDTO
{

    public DateTime StartDate { get; set; }
    public int InstructorId { get; set; }
    public int CourseId { get; set; }
    public string ExamStatus { get; set; }

}
