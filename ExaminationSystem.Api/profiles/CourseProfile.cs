
namespace ExaminationSystem.Api.profiles;

public class CourseProfile :Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseToReturnDto>();
    }
}
