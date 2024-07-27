namespace ExaminationSystem.Api.Models;

public class ExamQuestion:BaseModel
{
    public int ExamID { get; set; }
    public Exam Exam { get; set; }

    public int QuestionID { get; set; }
    public Question Question { get; set; }
}
