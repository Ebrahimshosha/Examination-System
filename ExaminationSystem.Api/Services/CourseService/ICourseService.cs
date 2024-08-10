namespace ExaminationSystem.Api.Services.CourseService;

public interface ICourseService
{
    bool ValidateIfInstructorGivesCourse(int courseId, int instructorId);
}
