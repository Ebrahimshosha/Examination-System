namespace ExaminationSystem.Api.Models;

public class Result : BaseModel
{
    public int Grade { get; set; }
    public int ExamId { get; set; }
    public Exam Exam { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }

}
