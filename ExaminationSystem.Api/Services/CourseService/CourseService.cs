using ExaminationSystem.Api.Exceptions;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ExaminationSystem.Api.Services.CourseService;

public class CourseService : ICourseService
{
    private readonly IGenericRepository<Course> _repository;

    public CourseService(IGenericRepository<Course> repository)
    {
        _repository = repository;
    }

    public bool ValidateIfInstructorGivesCourse(int courseId, int instructorId)
    {
        var instructorCourse = _repository.Get(cs => cs.Id == courseId && cs.InstructorId == instructorId);
        if (instructorCourse.Count() > 0) return true;
        throw new BusinessException(ErrorCode.InstructordidntGivethatCourse, "Instructor didn't Give that Course");
    }
}
