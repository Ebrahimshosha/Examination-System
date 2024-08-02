using AutoMapper;
using ExaminationSystem.Api.DTO.Course;
using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.profiles;

public class CourseProfile :Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseToReturnDto>();
    }
}
