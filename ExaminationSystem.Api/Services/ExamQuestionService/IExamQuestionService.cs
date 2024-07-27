using ExaminationSystem.Api.ViewModels.Exam;

namespace ExaminationSystem.Api.Services.ExamQuestionService;

public interface IExamQuestionService
{
    List<int> CreateExamQuestion(int ExamId, ICollection<int> QIds);
}
