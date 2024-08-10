

using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Helpers;
using ExaminationSystem.Api.Services.CourseService;
using ExaminationSystem.Api.Services.ResultService;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Services.ExamService;

public class ExamService : IExamService
{
    private readonly IGenericRepository<Exam> _examrepository;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IQuestionService _questionService;

    public ExamService(
        IGenericRepository<Exam> examRepository)
    {
        _examrepository = examRepository;
    }

    public ExamToReturnDto AddExamService(CreateExamDto model,int totalgrade)
    {
        var entity = _examrepository.Add(new Exam
        {
            StartDate = model.StartDate,
            InstructorId = model.InstructorId,
            CourseId = model.CourseId,
            TotalGrade = totalgrade,
            ExamStatus = Enum.Parse<ExamStatus>(model.ExamStatus, ignoreCase: true)
        });

        _examrepository.SaveChanges();

        var examToReturnDto = entity.MapOne<ExamToReturnDto>(); 

        return examToReturnDto;

    }
}
