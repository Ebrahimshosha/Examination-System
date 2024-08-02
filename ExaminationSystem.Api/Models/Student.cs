namespace ExaminationSystem.Api.Models;

public class Student:BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<StudentCourse> studentCourses { get;set; } = new HashSet<StudentCourse>();
    public ICollection<StudentExam> studentExams { get;set; } = new HashSet<StudentExam>();
}
