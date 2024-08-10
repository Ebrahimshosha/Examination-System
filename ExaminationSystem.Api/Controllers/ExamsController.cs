
using ExaminationSystem.Api.Services.ResultService;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Controllers;

public class ExamsController : BaseApiController
{
    private readonly IExamService _examService;
    private readonly IMapper _mapper;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IStudentExamService _studentExamService;
    private readonly IResultService _resultService;

    public ExamsController(
        IExamService examService,
        IMapper mapper,
        IExamQuestionService examQuestionService,
        IStudentExamService studentExamService,
        IResultService resultService)
    {
        _examService = examService;
        _mapper = mapper;
        _examQuestionService = examQuestionService;
        _studentExamService = studentExamService;
        _resultService = resultService;
    }

    //[HttpGet]
    //public ActionResult<IEnumerable<Exam>> GetAllExams()
    //{
    //    //var exams = _examService.GetAllExamsService();

    //    return Ok(exams);
    //}

    //[HttpGet("{id}")]
    //public ActionResult<Exam> GetExamById(int id)
    //{
    //    //var exam = _examService.GetExamServiceById(id);
    //    //return Ok(exam);
    //}

    [HttpPost("Manual")]
    public ActionResult<ExamToReturnDto> CreateManualExam(CreateManualExamViewModel viewModel)
    {
        var examDto = _mapper.Map<ExamManualDto>(viewModel);

        var examToReturnDto = _examService.CreateManualExamService(examDto);

        return Ok(examToReturnDto);
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
        var examDTO = _mapper.Map<ExamDTO>(viewModel);

        var exam = _examService.UpdateExamService(id, examDTO);

        return exam;
    }

    [HttpDelete]
    public ActionResult DeleteExamById(int id)
    {
        _examService.DeleteExamService(id);
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
    public ActionResult<int> StudentSubmitExam(int StudentId, int examId, List<quesrtionsAnswersViewModel> quesrtionsAnswersViewModel)
    {
        var Grede = _examService.StudentSubmitExam(StudentId, examId, quesrtionsAnswersViewModel);
        return Ok(Grede);
    }

}
