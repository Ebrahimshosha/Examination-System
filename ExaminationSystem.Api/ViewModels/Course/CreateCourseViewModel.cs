namespace ExaminationSystem.Api.ViewModels.Course;

public class CreateCourseViewModel
{
    public string Name { get; set; }
    public int CreditHours { get; set; }

    public int InstructorId { get; set; }
}
