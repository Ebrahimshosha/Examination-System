using AutoMapper;
using ExaminationSystem.Api.DTO.Exam;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.Services.ExamService;
using ExaminationSystem.Api.Services.StudentExamService;
using ExaminationSystem.Api.ViewModels.Exam;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace ExaminationSystem.Api.Controllers;

public class ExamsController : BaseApiController
{
    private readonly IExamService _examService;
    private readonly IMapper _mapper;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IStudentExamService _studentExamService;
    private readonly IGenericRepository<Exam> _examrepository;

    public ExamsController(
        IExamService examService,
        IMapper mapper,
        IExamQuestionService examQuestionService,
        IStudentExamService studentExamService,
        IGenericRepository<Exam> Examrepository)
    {
        _examService = examService;
        _mapper = mapper;
        _examQuestionService = examQuestionService;
        _studentExamService = studentExamService;
        _examrepository = Examrepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Exam>> GetAllExams()
    {
        return _examrepository.GetAll().ToList();

    }

    [HttpGet("{id}")]
    public ActionResult<Exam> GetExamById(int id)
    {
        var exam = _examrepository.GetByID(id);
        return Ok(exam);
    }

    [HttpPost("Manual")]
    public ActionResult<ExamToReturnDto> CreateManualExam(CreateManualExamViewModel viewModel)
    {
        var examDto = _mapper.Map<ExamManualDto>(viewModel);

        var examToReturnDto = _examService.CreateManualExamService(examDto);

        return examToReturnDto;
    }

    [HttpPost("Automatic")]
    public ActionResult<ExamToReturnDto> CreateAutomaticExam(CreateAutomaticExamViewModel viewModel)
    {
        var examDto = _mapper.Map<ExamAutomaticDto>(viewModel);

        var examToReturnDto = _examService.CreateAutomaticExamService(examDto);

        return examToReturnDto;
    }

    [HttpPut]
    public Exam UpdateExam(int id, CreateExamViewModel viewModel)
    {
        var exam = _examrepository.GetByID(id);
        exam.Id = id;
        exam.CourseId = viewModel.CourseId;
        exam.InstructorId = viewModel.InstructorId;
        exam.StartDate = viewModel.StartDate;

        _examrepository.Update(exam);
        _examrepository.SaveChanges();

        return exam;
    }

    [HttpDelete]
    public ActionResult DeleteExamById(int id)
    {
        var exam = _examrepository.GetByID(id);
        _examrepository.Delete(exam);
        _examrepository.SaveChanges();
        return Ok();
    }

    [HttpGet]
    public ActionResult<ExamToReturnDto> TakeExam(int studentId, int Courseid, string examStatus)
    {
        var exam = _examService.TakeExam(studentId, Courseid, examStatus);

        if (exam == null) { return BadRequest("Can not take more than One final to the same course"); }
        
        var exanToReturnDto = _mapper.Map<ExamToReturnDto>(exam);

        return Ok(exanToReturnDto);
    }

    [HttpPost]
    public ActionResult<StudentExam> AddStudentResult(int StudentId, int examId,int Result)
    {
        var studentexam = _studentExamService.AddStudentResult(StudentId, examId, Result);
        return Ok(studentexam);
    }

    [HttpGet]
    public ActionResult<StudentExam> GetStudentResult(int StudentId,int examId)
    {
        var studentexam = _studentExamService.GetStudentResult(StudentId, examId);
        return Ok(studentexam);
    }

}
