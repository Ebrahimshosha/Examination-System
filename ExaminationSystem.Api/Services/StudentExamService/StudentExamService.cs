using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.Services.StudentExamService;

public class StudentExamService : IStudentExamService
{
    private readonly IGenericRepository<StudentExam> _repository;

    public StudentExamService(IGenericRepository<StudentExam> repository)
    {
        _repository = repository;
    }
    public StudentExam AddStudentResult(int StudentId, int examId, int Result)
    {
        var studentExam = new StudentExam { StudentId = StudentId, ExamId = examId, Result = Result };
        var studentexam = _repository.Add(studentExam);
        _repository.SaveChanges();
        return studentexam;
    }
    public StudentExam GetStudentResult(int StudentId, int examId)
    {
        var studentExam = _repository.Get(e => e.ExamId == examId && e.StudentId == StudentId).FirstOrDefault();
        return studentExam;
    }
}
