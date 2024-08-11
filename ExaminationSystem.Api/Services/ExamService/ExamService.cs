

using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Helpers;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.CourseService;
using ExaminationSystem.Api.Services.ResultService;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Services.ExamService;

public class ExamService : IExamService
{
    private readonly IGenericRepository<Exam> _examrepository;

    public ExamService(IGenericRepository<Exam> examRepository)
    {
        _examrepository = examRepository;
    }

    public ExamToReturnDto AddExamService(CreateExamDto model, int totalgrade)
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


    public IEnumerable<Exam> GetAllExamsService()
    {
        return _examrepository.GetAll().ToList();
    }

    public Exam GetExamServiceById(int id)
    {
        return _examrepository.GetByID(id);
    }

    public Exam UpdateExam(int id, ExamDTO examDTO)
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

    public bool DeleteExam(int id)
    {
        var exam = _examrepository.GetByID(id);
        _examrepository.Delete(exam);
        _examrepository.SaveChanges();
        return true;
    }
    public Exam GetExam(int CourseId, string examStatus)
    {
        var status = Enum.Parse<ExamStatus>(examStatus);

        var exam = _examrepository.Get(x => x.CourseId == CourseId)
                                  .Where(x => x.ExamStatus == status)
                                  .FirstOrDefault();
        return exam;
    }
}
