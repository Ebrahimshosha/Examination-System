using ExaminationSystem.Api.DTO.Question;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.ChoiceService;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.Services.ExamService;
using ExaminationSystem.Api.ViewModels.CreateQuestionViewModel;
using System.Collections.Generic;

namespace ExaminationSystem.Api.Services.QuestionService;

public class QuestionService : IQuestionService
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
            Text = model.Text,
            Status = Enum.Parse<QStatus>(model.Status)

        });
        _questionRepository.SaveChanges();

        _examQuestionService.CreateExamQuestion(model.ExamId, question.Id);

        _choiceService.Addchoice(question.Id, model.Choices, model.RightAnswer);

        return question;
    }

    public int CalculateTotalGrade(ICollection<int> Qids)
    {
        var quetions = _questionRepository.Get(q => Qids.Contains(q.Id));
        var totalGrade = quetions.Sum(q => q.Grade);
        return totalGrade;
    }

    public List<int> CreateRandomQuestionsIds(int NumberOfQuestions, int CoureId)
    {
        int simple = NumberOfQuestions / 3;
        int medium = NumberOfQuestions / 3;
        int hard = NumberOfQuestions - simple - medium;

        var AllsimpleQuestions = _questionRepository.Get(q =>  q.Status == QStatus.simple);
        var AllmediumQuestions = _questionRepository.Get(q => q.Status == QStatus.medium);
        var AllhardQuestions = _questionRepository.Get(q =>  q.Status == QStatus.hard);

        var RandomQuestionsIds = new List<int>();

        RandomQuestionsIds.AddRange(CreateSpecificRandomQuestionsIds(AllsimpleQuestions, simple));

        RandomQuestionsIds.AddRange(CreateSpecificRandomQuestionsIds(AllmediumQuestions, medium));

        RandomQuestionsIds.AddRange(CreateSpecificRandomQuestionsIds(AllhardQuestions, hard));

        return RandomQuestionsIds;

    }

    public List<int> CreateSpecificRandomQuestionsIds(IQueryable<Question> questions, int NumberOfQuestions)
    {

        List<int> AllQuestionsIds = questions.Select(a => a.Id).ToList();

        Random random = new Random();

        List<int> RandomQuestionsIds = new List<int>();

        for (int i = 0; i < NumberOfQuestions; i++)
        {
            int randomIndex = random.Next(0, questions.Count());
            // Check if randomIndex is exists before
            RandomQuestionsIds.Add(AllQuestionsIds[randomIndex]);
        }

        return RandomQuestionsIds;

    }
}
