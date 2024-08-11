using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Helpers;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.CourseService;
using ExaminationSystem.Api.Services.ResultService;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Mediators.ExamMediator;

public class ExamMediator : IExamMediator
{
    private readonly IExamService _examService;
    private readonly ICourseService _courseService;
    private readonly IQuestionService _questionService;
    private readonly IStudentExamService _studentExamService;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IChoiceService _choiceService;
    private readonly IResultService _resultService;

    public ExamMediator(IExamService examService,
        ICourseService courseService,
        IQuestionService questionService,
        IStudentExamService studentExamService,
        IExamQuestionService examQuestionService,
        IChoiceService choiceService,
        IResultService resultService,
        IGenericRepository<Choice> choiceRepository,
        IGenericRepository<StudentExam> studentExamRepository
        )
    {
        _examService = examService;
        _courseService = courseService;
        _questionService = questionService;
        _studentExamService = studentExamService;
        _examQuestionService = examQuestionService;
        _choiceService = choiceService;
        _resultService = resultService;
    }

    private int CalculateTotalGrade(ICollection<int> QuestionsIDs)
    {
        return _questionService.CalculateTotalGrade(QuestionsIDs);
    }

    private ExamToReturnDto CreateExam(CreateExamDto model, ICollection<int> QuestionsIDs)
    {
        var IsInstructorGivesCourse = _courseService.ValidateIfInstructorGivesCourse(model.CourseId, model.InstructorId);
        if (IsInstructorGivesCourse)
        {
            var totalgrade = CalculateTotalGrade(QuestionsIDs);
            var examToReturnDto = _examService.AddExamService(model, totalgrade);
            var Qids = _examQuestionService.CreateExamQuestion(examToReturnDto.Id, QuestionsIDs);

            return examToReturnDto;
        }
        return null;
    }
    public ExamToReturnDto CreateManualExam(ExamManualDto model)
    {
        var createExamDto = model.MapOne<CreateExamDto>();

        var examToReturnDto = CreateExam(createExamDto, model.QuestionsIDs);

        return examToReturnDto;
    }

    public ExamToReturnDto CreateAutomaticExam(ExamAutomaticDto model)
    {
        var RandomQuestionsIds = _questionService.CreateRandomQuestionsIds(model.NunmberOfQuestions, model.CourseId);

        var createExamDTO = model.MapOne<CreateExamDto>();

        var examToReturnDto = CreateExam(createExamDTO, RandomQuestionsIds);

        return examToReturnDto;

    }

    public IEnumerable<Exam> GetAllExamsService()
    {
        return _examService.GetAllExamsService();
    }

    public Exam GetExamServiceById(int id)
    {
        return _examService.GetExamServiceById(id);
    }

    public Exam TakeExam(int studentId, int Courseid, string status)
    {
        var examStatus = Enum.Parse<ExamStatus>(status);

        var hasTakenFinalExam = _studentExamService.HasTakenFinalExam(studentId, Courseid, examStatus);

        if (!hasTakenFinalExam)
        {
            var exam = _examService.GetExam(Courseid, status);
            _studentExamService.AddStudentExam(exam.Id, studentId);
            return exam;
        }
        return null;
    }

    public Exam UpdateExam(int id, ExamDTO examDTO)
    {
        var exam = _examService.UpdateExam(id, examDTO);

        return exam;
    }

    public bool DeleteExam(int id)
    {
        return _examService.DeleteExam(id);
    }

    public int StudentSubmitExam(int StudentId, int examId, List<quesrtionsAnswersViewModel> quesrtionsAnswersViewModel)
    {
        var isSubmitted = _studentExamService.CkeckIfStudentSubmittedTheExam(examId, StudentId);

        if (!isSubmitted)
        {
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
                var choices = _choiceService.GetChoicesbyQid(Qids[i]);
                var RightAnswer = choices.Where(c => c.IsRightAnswer).Select(c => c.Text).FirstOrDefault();

                if (Answers[i].ToUpper() == RightAnswer!.ToUpper())
                {
                    Grade += _choiceService.GetChoicesbyQid(Qids[i]).FirstOrDefault()!.Question.Grade;
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
        return -1;
    }
}
