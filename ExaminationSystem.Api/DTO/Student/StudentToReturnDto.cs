
namespace ExaminationSystem.Api.DTO.Student;

public class StudentToReturnDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Course { get; set; }
    public List<int> ExamIds { get; set; }
}
