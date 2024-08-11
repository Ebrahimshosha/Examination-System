using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Helpers;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Mediators.ExamMediator;

public interface IExamMediator
{
    ExamToReturnDto CreateManualExam(ExamManualDto model);
    ExamToReturnDto CreateAutomaticExam(ExamAutomaticDto model);
    IEnumerable<Exam> GetAllExamsService();
    Exam GetExamServiceById(int id);
    Exam UpdateExam(int id, ExamDTO examDTO);
    public Exam TakeExam(int studentId, int Courseid, string status);
    public bool DeleteExam(int id);
    int StudentSubmitExam(int StudentId, int examId, List<quesrtionsAnswersViewModel> quesrtionsAnswersViewModel);
}
