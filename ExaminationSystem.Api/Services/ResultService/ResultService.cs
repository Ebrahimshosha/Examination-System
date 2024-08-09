using ExaminationSystem.Api.Exceptions;

namespace ExaminationSystem.Api.Services.ResultService;

public class ResultService : IResultService
{
    private readonly IGenericRepository<Result> _repository;

    public ResultService(IGenericRepository<Result> repository)
    {
        _repository = repository;
    }

    public void AddResultService(Result result)
    {
        _repository.Add(result);
        _repository.SaveChanges();
    }
    public int GetStudentResult(int StudentId, int examId)
    {
        var studentExam = _repository.Get(e => e.ExamId == examId && e.StudentId == StudentId).FirstOrDefault();
        return studentExam.Grade;
    }
}
