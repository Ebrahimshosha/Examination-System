using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.Services.StudentExamService;

public class StudentExamService : IStudentExamService
{
    private readonly IGenericRepository<StudentExam> _repository;
    private readonly IGenericRepository<StudentCourse> _studentCourserepository;
    private readonly IGenericRepository<StudentExam> _studentExamrepository;

    public StudentExamService(IGenericRepository<StudentExam> repository,
        IGenericRepository<StudentCourse> StudentCourserepository,
        IGenericRepository<StudentExam> StudentExamrepository
        )
    {
        _repository = repository;
        _studentCourserepository = StudentCourserepository;
        _studentExamrepository = StudentExamrepository;
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
                                .Get(s => s.Id == studentId && s.Exam.ExamStatus == ExamStatus.final && s.Exam.CourseId == Courseid && s.IsSubmitted)
                                .ToList();

            if (studentexams.Count > 0) return true;
            return false;

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
