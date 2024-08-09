using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Api.Services.VlidationIfTakenFinalExam;

public class ValidateIfTakenFinalExam : IValidateIfTakenFinalExam
{
    private readonly IGenericRepository<StudentCourse> _studentCourserepository;
    private readonly IGenericRepository<StudentExam> _studentExamrepository;
    private readonly IGenericRepository<Exam> _examrepository;

    public ValidateIfTakenFinalExam(
        IGenericRepository<StudentCourse> StudentCourserepository,
        IGenericRepository<StudentExam> StudentExamrepository,
        IGenericRepository<Exam> Examrepository)
    {
        _studentCourserepository = StudentCourserepository;
        _studentExamrepository = StudentExamrepository;
        _examrepository = Examrepository;
    }
    public bool HasTakenFinalExam(int studentId, int Courseid, ExamStatus status)
    {
        var studentCourse = _studentCourserepository.Get(x => x.StudentId == studentId && x.CourseId == Courseid);

        if (studentCourse.Count() == 0)
        {
            throw new BusinessException(ErrorCode.StudentHasNotRegisteredForThisCourse, "This student has not registered for this course");
        }

        if (status == ExamStatus.final)
        {
            var studentexams = _studentExamrepository
                                .Get(s => s.Id == studentId && s.Exam.ExamStatus == ExamStatus.final && s.Exam.CourseId == Courseid)
                                .ToList();

            if (studentexams.Count > 0) return true;

        }
        return false;
    }
}
