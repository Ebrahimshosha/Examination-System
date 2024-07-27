using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.Services.ExamService;
using ExaminationSystem.Api.ViewModels.Exam;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Api.Controllers;

public class ExamsController : BaseApiController
{
    private readonly IExamService _examService;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IGenericRepository<Exam> _examrepository;

    public ExamsController(
        IExamService examService,
        IExamQuestionService examQuestionService,
        IGenericRepository<Exam> Examrepository)
    {
        _examService = examService;
        _examQuestionService = examQuestionService;
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

    [HttpPost]
    public Exam AddExam(CreateExamViewModel viewModel)
    {
        var exam = _examService.CreateExamService(viewModel);

        return exam;
    }
    
    [HttpPut]
    public Exam UpdateExam(int id,CreateExamViewModel viewModel)
    {
        var exam = _examrepository.GetByID(id);
        exam.Id= id;
        exam.CourseId= viewModel.CourseId;
        exam.InstructorId= viewModel.InstructorId;
        exam.StartDate= viewModel.StartDate;
        exam.TotalGrade= viewModel.TotalGrade;

        _examrepository.Update(exam);
        _examrepository.SaveChanges();

        return exam;
    }

    [HttpDelete]
    public ActionResult DeleteExamById(int id) 
    {
       var exam =  _examrepository.GetByID(id);
        _examrepository.Delete(exam);
        _examrepository.SaveChanges();
        return Ok();
    }
}
