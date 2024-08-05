using AutoMapper;
using ExaminationSystem.Api.DTO.Exam;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.Services.QuestionService;
using ExaminationSystem.Api.ViewModels.Exam;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ExaminationSystem.Api.Services.ExamService;

public class ExamService : IExamService
{
    private readonly IGenericRepository<Exam> _examrepository;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IValidateIfTakenFinalExam _validateIfTakenFinalExam;
    private readonly IQuestionService _questionService;
    private readonly IMapper _mapper;

    public ExamService(
        IGenericRepository<Exam> Examrepository,
        IExamQuestionService examQuestionService,
        IValidateIfTakenFinalExam validateIfTakenFinalExam
        , IQuestionService questionService,
        IMapper mapper)
    {
        _examrepository = Examrepository;
        _examQuestionService = examQuestionService;
        _validateIfTakenFinalExam = validateIfTakenFinalExam;
        _questionService = questionService;
        _mapper = mapper;
    }


    public ExamToReturnDto CreateManualExamService(ExamManualDto model)
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

    public ExamToReturnDto CreateAutomaticExamService(ExamAutomaticDto model)
    {
        var RandomQuestionsIds = _questionService.CreateRandomQuestionsIds(model.NunmberOfQuestions,model.CourseId);

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

    public Exam TakeExam(int studentId, int Courseid, string status)
    {
        var examStatus = Enum.Parse<ExamStatus>(status);

        var hasTakenFinalExam = _validateIfTakenFinalExam.HasTakenFinalExam(studentId, Courseid, examStatus);

        if (!hasTakenFinalExam)
        {
            var exam = _examrepository.Get(x => x.CourseId == Courseid)
                .Where(x => x.ExamStatus == examStatus)
                .FirstOrDefault();

            return exam;
        }

        return null;
    }
    
}
