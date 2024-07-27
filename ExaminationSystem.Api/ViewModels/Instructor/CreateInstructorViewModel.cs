namespace ExaminationSystem.Api.ViewModels.Instructor;

public class CreateInstructorViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
