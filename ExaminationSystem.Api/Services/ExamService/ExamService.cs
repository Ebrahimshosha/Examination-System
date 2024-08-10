

using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Services.CourseService;
using ExaminationSystem.Api.Services.ResultService;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Services.ExamService;

public class ExamService : IExamService
{
    private readonly IGenericRepository<Exam> _examrepository;
    private readonly IGenericRepository<Choice> _choiceRepository;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IResultService _resultService;
    private readonly IGenericRepository<StudentExam> _studentExamRepository;
    private readonly IQuestionService _questionService;
    private readonly IStudentExamService _studentExamService;
    private readonly ICourseService _courseService;
    private readonly IMapper _mapper;

    public ExamService(
        IGenericRepository<Exam> examRepository,
        IGenericRepository<Choice> ChoiceRepository,
        IExamQuestionService examQuestionService,
        IResultService resultService,
        IGenericRepository<StudentExam> studentExamRepository,
        IQuestionService questionService,
        IStudentExamService studentExamService,
        ICourseService courseService,
        IMapper mapper)
    {
        _examrepository = examRepository;
        _choiceRepository = ChoiceRepository;
        _examQuestionService = examQuestionService;
        _resultService = resultService;
        _studentExamRepository = studentExamRepository;
        _questionService = questionService;
        _studentExamService = studentExamService;
        _courseService = courseService;
        _mapper = mapper;
    }

    public IEnumerable<Exam> GetAllExamsService()
    {
        return _examrepository.GetAll().ToList();

    }

    public Exam GetExamServiceById(int id)
    {
        var exam = _examrepository.GetByID(id);
        return exam;
    }

    public ExamToReturnDto CreateManualExamService(ExamManualDto model)
    {
        var IsInstructorGivesCourse = _courseService.ValidateIfInstructorGivesCourse(model.CourseId, model.InstructorId);

        if (IsInstructorGivesCourse)
        {
            var totalgrade = _questionService.CalculateTotalGrade(model.QuestionsIDs);
            var entity = _examrepository.Add(new Exam
            {
                StartDate = model.StartDate,
                InstructorId = model.InstructorId,
                CourseId = model.CourseId,
                TotalGrade = totalgrade,
                ExamStatus = Enum.Parse<ExamStatus>(model.ExamStatus/*, ignoreCase: true*/)
            });

            _examrepository.SaveChanges();

            var Qids = _examQuestionService.CreateExamQuestion(entity.Id, model.QuestionsIDs);

            var examToReturnDto = _mapper.Map<ExamToReturnDto>(entity);

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

            var entity = _examrepository.Add(new Exam
            {
                StartDate = model.StartDate,
                InstructorId = model.InstructorId,
                CourseId = model.CourseId,
                TotalGrade = totalgrade,
                ExamStatus = Enum.Parse<ExamStatus>(model.ExamStatus/*, ignoreCase: true*/)
            });


            _examrepository.SaveChanges();

            var Qids = _examQuestionService.CreateExamQuestion(entity.Id, RandomQuestionsIds);

            var examToReturnDto = _mapper.Map<ExamToReturnDto>(entity);

            return examToReturnDto;
        }
        return null;
    }

    public Exam TakeExam(int studentId, int Courseid, string status)
    {
        var examStatus = Enum.Parse<ExamStatus>(status);

        var hasTakenFinalExam = _studentExamService.HasTakenFinalExam(studentId, Courseid, examStatus);

        if (!hasTakenFinalExam)
        {
            var exam = _examrepository.Get(x => x.CourseId == Courseid)
                .Where(x => x.ExamStatus == examStatus)
                .FirstOrDefault();

            _studentExamService.AddStudentExam(exam.Id, studentId);
            return exam;
        }
        return null;
    }

    public Exam UpdateExamService(int id, ExamDTO examDTO)
    {
        var exam = _examrepository.GetByID(id);

        exam.Id = id;
        exam.CourseId = examDTO.CourseId;
        exam.InstructorId = examDTO.InstructorId;
        exam.StartDate = examDTO.StartDate;
        exam.ExamStatus = Enum.Parse<ExamStatus>(examDTO.ExamStatus);

        _examrepository.Update(exam);
        _examrepository.SaveChanges();

        return exam;
    }

    public bool DeleteExamService(int id)
    {
        var exam = _examrepository.GetByID(id);
        _examrepository.Delete(exam);
        _examrepository.SaveChanges();
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
