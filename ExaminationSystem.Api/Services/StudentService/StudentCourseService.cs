using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.Services.StudentService;

public class StudentCourseService : IStudentCourseService
{
    private readonly IGenericRepository<StudentCourse> _repository;

    public StudentCourseService(IGenericRepository<StudentCourse> repository)
    {
        _repository = repository;
    }

    public StudentCourse EnrollCourseToStudent(int studentId, int CourseId)
    {
        var studentCourse = _repository.Add(new StudentCourse
        {
            StudentId = studentId,
            CourseId = CourseId
        });
        _repository.SaveChanges();
        return studentCourse;
    }
   

}
