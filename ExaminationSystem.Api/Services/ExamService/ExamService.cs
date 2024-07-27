using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.ViewModels.Exam;

namespace ExaminationSystem.Api.Services.ExamService;

public class ExamService:IExamService
{
    private readonly IGenericRepository<Exam> _examrepository;
    private readonly IExamQuestionService _examQuestionService;

    public ExamService(
        IGenericRepository<Exam> Examrepository,
        IExamQuestionService examQuestionService)
    {
        _examrepository = Examrepository;
        _examQuestionService = examQuestionService;
    }

    public Exam CreateExamService(CreateExamViewModel viewModel)
    {
        var entity = _examrepository.Add(new Exam
        {
            StartDate = viewModel.StartDate,
            InstructorId = viewModel.InstructorId,
            CourseId = viewModel.CourseId,
            TotalGrade = viewModel.TotalGrade
        });
        _examrepository.SaveChanges();
        var Qids = _examQuestionService.CreateExamQuestion(entity.Id, viewModel.QuestionsIDs);
        return entity;

    }
}
