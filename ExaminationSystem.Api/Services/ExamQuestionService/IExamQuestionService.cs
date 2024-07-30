using ExaminationSystem.Api.ViewModels.Exam;

namespace ExaminationSystem.Api.Services.ExamQuestionService;

public interface IExamQuestionService
{
    void CreateExamQuestion(int ExamId, int QId);
    List<int> CreateExamQuestion(int ExamId, ICollection<int> QIds);
}
