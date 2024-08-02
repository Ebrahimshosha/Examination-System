using AutoMapper;
using ExaminationSystem.Api.DTO.Exam;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.ViewModels.Exam;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ExaminationSystem.Api.Services.ExamService;

public class ExamService : IExamService
{
    private readonly IGenericRepository<Exam> _examrepository;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IGenericRepository<StudentCourse> _studentCourseRepository;
    private readonly IMapper _mapper;

    public ExamService(
        IGenericRepository<Exam> Examrepository,
        IExamQuestionService examQuestionService,
        IGenericRepository<StudentCourse> studentCourseRepository,
        IMapper mapper)
    {
        _examrepository = Examrepository;
        _examQuestionService = examQuestionService;
        _studentCourseRepository = studentCourseRepository;
        _mapper = mapper;
    }

    public ExamToReturnDto CreateExamService(ExamDto model)
    {

        var entity = _examrepository.Add(new Exam
        {
            StartDate = model.StartDate,
            InstructorId = model.InstructorId,
            CourseId = model.CourseId,
            TotalGrade = model.TotalGrade,
            ExamStatus = Enum.Parse<ExamStatus>(model.ExamStatus/*, ignoreCase: true*/)
        });

        var isFinalExamExists = _examrepository.Get(e => e.CourseId == model.CourseId).Any(e => e.ExamStatus == entity.ExamStatus);
        
        if (!isFinalExamExists)
        {
            _examrepository.SaveChanges();

            var Qids = _examQuestionService.CreateExamQuestion(entity.Id, model.QuestionsIDs);

            var examToReturnDto = _mapper.Map<ExamToReturnDto>(entity);

            return examToReturnDto;
        }
        return null;
    }


    public Exam TakeExam(int studentId, int Courseid,string status)
    {
        var studentCourse = _studentCourseRepository.Get(x => x.StudentId == studentId)
                                                    .FirstOrDefault(sc => sc.CourseId == Courseid);
        
        var examStatus = Enum.Parse<ExamStatus>(status);

        if (studentCourse != null)
        {
            var exam = _examrepository.Get(x => x.CourseId == Courseid)
                .Where(x=>x.ExamStatus==examStatus)
                .FirstOrDefault();
            return exam;
        }

        return null;

    }
}
