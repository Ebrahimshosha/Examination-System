using ExaminationSystem.Api.Services.ResultService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Api.Controllers;
public class ResultController : BaseApiController
{
    private readonly IResultService _resultService;

    public ResultController(IResultService resultService)
    {
        _resultService = resultService;
    }

    [HttpGet]
    public ActionResult<int> GetStudentResult(int StudentId, int examId)
    {
        var grade = _resultService.GetStudentResult(StudentId, examId);
        return Ok(grade);
    }
}
