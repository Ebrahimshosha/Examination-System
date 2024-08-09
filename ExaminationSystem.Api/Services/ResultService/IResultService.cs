namespace ExaminationSystem.Api.Services.ResultService;

public interface IResultService
{
    void AddResultService(Result result);
    int GetStudentResult(int StudentId, int examId);


}
