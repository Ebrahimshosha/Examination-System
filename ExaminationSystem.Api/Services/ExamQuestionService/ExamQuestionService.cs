using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.Exam;
using System.Security.Cryptography;

namespace ExaminationSystem.Api.Services.ExamQuestionService;

public class ExamQuestionService : IExamQuestionService
{
    private readonly IGenericRepository<ExamQuestion> _examQuestionrepository;

    public ExamQuestionService(IGenericRepository<ExamQuestion> ExamQuestionrepository)
    {
        _examQuestionrepository = ExamQuestionrepository;
    }

    public void CreateExamQuestion(int ExamId, int QuestionID)
    {

        var Q = _examQuestionrepository.Add(new ExamQuestion
        {
            ExamID = ExamId,
            QuestionID = QuestionID

        });

        _examQuestionrepository.SaveChanges();
    }

    public List<int> CreateExamQuestion(int ExamId, ICollection<int> QuestionsIDs)
    {
        List<int> Oids = new List<int>();

        foreach (var id in QuestionsIDs)
        {
            var Q = _examQuestionrepository.Add(new ExamQuestion
            {
                ExamID = ExamId,
                QuestionID = id

            });
            Oids.Add(Q.QuestionID);
        }
        _examQuestionrepository.SaveChanges();

        return Oids;
    }
}
