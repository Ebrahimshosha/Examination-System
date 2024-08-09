using ExaminationSystem.Api.Exceptions;
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
    public void AddStudentExam(int examId, int studentId)
    {
        _repository.Add(new StudentExam() { ExamId = examId, StudentId = studentId });
        _repository.SaveChanges();
    }

    public StudentExam AddStudentResult(int StudentId, int examId)
    {

        var studentExam = new StudentExam { StudentId = StudentId, ExamId = examId };
        var studentexam = _repository.Add(studentExam);
        _repository.SaveChanges();
        return studentexam;
    }

    
    public bool CkeckIfStudentSubmittedTheExam(int examId, int StudentId)
    {

        var studentExam = _repository.Get(s => s.ExamId == examId && s.StudentId == StudentId && s.IsSubmitted);
        if (studentExam is not null)
        {
            return true;
        }
        return false;
    }

    
    public void SubmitExam(int examId, int StudentId)
    {
        _repository.Update(new StudentExam()
        {
            ExamId = examId,
            StudentId = StudentId,
            IsSubmitted = true
        });
        _repository.SaveChanges();
    }
    public bool CheckstudentTakethisExamBefore(int examId, int StudentId)
    {
        var studentResult = _repository.Get(r => r.ExamId == examId && r.StudentId == StudentId);

        if (studentResult == null)
        {
            throw new BusinessException(ErrorCode.NotallowedToTakeSameExamAnotherTime, "Not allowed to take same exam another time");
        }
        return true;
    }
}
