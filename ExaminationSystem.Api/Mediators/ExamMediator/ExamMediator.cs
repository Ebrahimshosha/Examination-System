using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Helpers;
using ExaminationSystem.Api.Services.CourseService;
using ExaminationSystem.Api.Services.ResultService;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Mediators.ExamMediator;

public class ExamMediator: IExamMediator
{
    private readonly IExamService _examService;
    private readonly ICourseService _courseService;
    private readonly IQuestionService _questionService;
    private readonly IStudentExamService _studentExamService;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IResultService _resultService;
    private readonly IGenericRepository<Exam> _examRepository;
    private readonly IGenericRepository<Choice> _choiceRepository;
    private readonly IGenericRepository<StudentExam> _studentExamRepository;

    public ExamMediator(IExamService examService,
        ICourseService courseService,
        IQuestionService questionService,
        IStudentExamService studentExamService,
        IExamQuestionService examQuestionService,
        IResultService resultService,
        IGenericRepository<Exam> examRepository,
        IGenericRepository<Choice> choiceRepository,
        IGenericRepository<StudentExam> studentExamRepository
        )
    {
        _examService = examService;
        _courseService = courseService;
        _questionService = questionService;
        _examRepository = examRepository;
        _studentExamService = studentExamService;
        _examQuestionService = examQuestionService;
        _resultService = resultService;
        _choiceRepository = choiceRepository;
        _studentExamRepository = studentExamRepository;
    }

    //public ExamToReturnDto AddExam()
    //{
    //    /////
    //}

    public ExamToReturnDto CreateManualExam(ExamManualDto model)
    {
        var IsInstructorGivesCourse = _courseService.ValidateIfInstructorGivesCourse(model.CourseId, model.InstructorId);

        if (IsInstructorGivesCourse)
        {
            var totalgrade = _questionService.CalculateTotalGrade(model.QuestionsIDs);
            var createExamDTO = model.MapOne<CreateExamDto>();
            var examToReturnDto = _examService.AddExamService(createExamDTO, totalgrade);
            var Qids = _examQuestionService.CreateExamQuestion(examToReturnDto.Id, model.QuestionsIDs);

            return examToReturnDto;
        }
        return null;
    }

    public ExamToReturnDto CreateAutomaticExamService(ExamAutomaticDto model)
    {
        var IsInstructorGivesCourse = _courseService.ValidateIfInstructorGivesCourse(model.CourseId, model.InstructorId);

        if (IsInstructorGivesCourse)
        {
            var RandomQuestionsIds = _questionService.CreateRandomQuestionsIds(model.NunmberOfQuestions, model.CourseId);

            var totalgrade = _questionService.CalculateTotalGrade(RandomQuestionsIds);

            var createExamDTO = model.MapOne<CreateExamDto>();

            var examToReturnDto = _examService.AddExamService(createExamDTO, totalgrade);

            var Qids = _examQuestionService.CreateExamQuestion(examToReturnDto.Id, RandomQuestionsIds);

            return examToReturnDto;
        }
        return null;
    }
    public IEnumerable<Exam> GetAllExamsService()
    {
        return _examRepository.GetAll().ToList();
    }

    public Exam GetExamServiceById(int id)
    {
        var exam = _examRepository.GetByID(id);
        return exam;
    }

    public Exam TakeExam(int studentId, int Courseid, string status)
    {
        var examStatus = Enum.Parse<ExamStatus>(status);

        var hasTakenFinalExam = _studentExamService.HasTakenFinalExam(studentId, Courseid, examStatus);

        if (!hasTakenFinalExam)
        {
            var exam = _examRepository.Get(x => x.CourseId == Courseid)
                .Where(x => x.ExamStatus == examStatus)
                .FirstOrDefault();

            _studentExamService.AddStudentExam(exam.Id, studentId);
            return exam;
        }
        return null;
    }

    public Exam UpdateExamService(int id, ExamDTO examDTO)
    {
        var exam = _examRepository.GetByID(id);

        exam.Id = id;
        exam.CourseId = examDTO.CourseId;
        exam.InstructorId = examDTO.InstructorId;
        exam.StartDate = examDTO.StartDate;
        exam.ExamStatus = Enum.Parse<ExamStatus>(examDTO.ExamStatus);

        _examRepository.Update(exam);
        _examRepository.SaveChanges();

        return exam;
    }

    public bool DeleteExamService(int id)
    {
        var exam = _examRepository.GetByID(id);
        _examRepository.Delete(exam);
        _examRepository.SaveChanges();
        return true;
    }

    public int StudentSubmitExam(int StudentId, int examId, List<quesrtionsAnswersViewModel> quesrtionsAnswersViewModel)
    {
        var studentExam = _studentExamRepository.Get(s => s.ExamId == examId && s.StudentId == StudentId && s.IsSubmitted);
        if (studentExam == null) { throw new BusinessException(ErrorCode.None, "Student didn't Submitt in this exam"); }

        var isTaken = _studentExamService.CheckstudentTakethisExamBefore(examId, StudentId);
        if (isTaken)
        {
            throw new BusinessException(ErrorCode.NotallowedToTakeSameExamAnotherTime, "Student already taken this exam");
        }
        var Qids = quesrtionsAnswersViewModel.Select(qa => qa.Qid).ToList();
        var Answers = quesrtionsAnswersViewModel.Select(qa => qa.Answer).ToList();
        int Grade = 0;


        for (int i = 0; i < Qids.Count; i++)
        {
            var choices = _choiceRepository.Get(c => c.QuestionID == Qids[i]);
            var RightAnswer = choices.Where(c => c.IsRightAnswer).Select(c => c.Text).FirstOrDefault();

            if (Answers[i].ToUpper() == RightAnswer!.ToUpper())
            {
                Grade += _choiceRepository.Get(c => c.QuestionID == Qids[i]).FirstOrDefault()!.Question.Grade;
            }
        }
        _resultService.AddResultService(new Result
        {
            ExamId = examId,
            Grade = Grade,
            StudentId = StudentId
        });

        _studentExamService.SubmitExam(examId, StudentId);

        return Grade;
    }
}
