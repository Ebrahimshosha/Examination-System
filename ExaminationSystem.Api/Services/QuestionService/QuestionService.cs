using ExaminationSystem.Api.DTO.Question;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.ChoiceService;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.Services.ExamService;
using ExaminationSystem.Api.ViewModels.CreateQuestionViewModel;

namespace ExaminationSystem.Api.Services.QuestionService;

public class QuestionService: IQuestionService
{
    private readonly IGenericRepository<Question> _questionRepository;
    private readonly IGenericRepository<Choice> _choiceRepository;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IChoiceService _choiceService;

    public QuestionService(
        IGenericRepository<Question> QuestionRepository,
        IGenericRepository<Choice> ChoiceRepository,
        IExamQuestionService examQuestionService,
        IChoiceService choiceService)
    {
        _questionRepository = QuestionRepository;
        _choiceRepository = ChoiceRepository;
        _examQuestionService = examQuestionService;
        _choiceService = choiceService;
    }

    public Question AddQuestion(CreateQuestionDto model)
    {
        var question = _questionRepository.Add(new Question
        {
            Grade = model.Grade,
            Text = model.Text

        });
        _questionRepository.SaveChanges();

        _examQuestionService.CreateExamQuestion(model.ExamId, question.Id);

        _choiceService.Addchoice(question.Id, model.Choices, model.RightAnswer);

        return question;

    }
}
