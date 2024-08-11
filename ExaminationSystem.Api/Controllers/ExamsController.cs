
using ExaminationSystem.Api.Helpers;
using ExaminationSystem.Api.Mediators.ExamMediator;
using ExaminationSystem.Api.Services.ResultService;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Controllers;

public class ExamsController : BaseApiController
{
    private readonly IExamMediator _mediator;

    public ExamsController(IExamMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Exam>> GetAllExams()
    {
        var exams = _mediator.GetAllExamsService();

        return Ok(exams);
    }

    [HttpGet("{id}")]
    public ActionResult<Exam> GetExamById(int id)
    {
        var exam = _mediator.GetExamServiceById(id);

        return Ok(exam);
    }

    [HttpPost("Manual")]
    public ActionResult<ExamToReturnDto> CreateManualExam(CreateManualExamViewModel viewModel)
    {
        var examDto = viewModel.MapOne<ExamManualDto>();

        var examToReturnDto = _mediator.CreateManualExam(examDto);

        return Ok(examToReturnDto);
    }

    [HttpPost("Automatic")]
    public ActionResult<ExamToReturnDto> CreateAutomaticExam(CreateAutomaticExamViewModel viewModel)
    {
        var examDto = viewModel.MapOne<ExamAutomaticDto>();

        var examToReturnDto = _mediator.CreateAutomaticExam(examDto);

        return examToReturnDto;
    }

    [HttpPut]
    public Exam UpdateExam(int id, CreateExamViewModel viewModel)
    {
        var examDTO = viewModel.MapOne<ExamDTO>();

        var exam = _mediator.UpdateExam(id, examDTO);

        return exam;
    }

    [HttpDelete]
    public ActionResult DeleteExamById(int id)
    {
        _mediator.DeleteExam(id);
        return Ok();
    }

    [HttpGet]
    public ActionResult<ExamToReturnDto> TakeExam(int studentId, int Courseid, string examStatus)
    {
        var exam = _mediator.TakeExam(studentId, Courseid, examStatus);

        if (exam == null) { return BadRequest("Can not take more than One final to the same course"); }

        var exanToReturnDto = exam.MapOne<ExamToReturnDto>();

        return Ok(exanToReturnDto);
    }

    [HttpPost]
    public ActionResult<int> StudentSubmitExam(int StudentId, int examId, List<quesrtionsAnswersViewModel> quesrtionsAnswersViewModel)
    {
        var Grede = _mediator.StudentSubmitExam(StudentId, examId, quesrtionsAnswersViewModel);
        return Ok(Grede);
    }
}
