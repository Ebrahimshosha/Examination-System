using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.Services.StudentExamService;

public interface IStudentExamService
{
    StudentExam AddStudentResult(int StudentId, int examId, int Result);

    StudentExam GetStudentResult(int StudentId, int examId);
}
