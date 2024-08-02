namespace ExaminationSystem.Api.Models;

public class Exam : BaseModel
{
    public DateTime StartDate { get; set; }

    public int TotalGrade { get; set; }

    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public ExamStatus ExamStatus { get; set; }

    public HashSet<ExamQuestion> ExamQuestions { get; set; } = new HashSet<ExamQuestion>();
    public HashSet<StudentExam> StudentExams { get; set; } = new HashSet<StudentExam>();
}
