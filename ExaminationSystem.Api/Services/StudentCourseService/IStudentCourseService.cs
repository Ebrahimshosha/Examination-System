using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.Services.StudentService;

public interface IStudentCourseService
{
    StudentCourse EnrollCourseToStudent(int studentId, int CourseId);


}
