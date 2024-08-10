using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.Services.StudentExamService;

public interface IStudentExamService
{
    bool CkeckIfStudentSubmittedTheExam(int examId, int StudentId);
    void AddStudentExam(int examId, int studentId);
    void SubmitExam(int examId, int StudentId);
    bool CheckstudentTakethisExamBefore(int examId, int StudentId);
    bool HasTakenFinalExam(int studentId, int Courseid, ExamStatus status);
}
