
namespace ExaminationSystem.Api.profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentToReturnDto>();
    }
}
