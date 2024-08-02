namespace ExaminationSystem.Api.Models;

public class Course:BaseModel
{
    public string Name { get; set; }
    public int CreditHours { get; set; }

    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; }
    public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
    public ICollection<StudentCourse> studentCourses { get; set; } = new HashSet<StudentCourse>();

}
