using ExaminationSystem.Api.Exceptions;
using ExaminationSystem.Api.Helpers;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;

namespace ExaminationSystem.Api.Mediators.ExamMediator;

public interface IExamMediator
{
    ExamToReturnDto CreateManualExamService(ExamManualDto model);
    ExamToReturnDto CreateAutomaticExamService(ExamAutomaticDto model);
    IEnumerable<Exam> GetAllExamsService();
    Exam GetExamServiceById(int id);
    public Exam TakeExam(int studentId, int Courseid, string status);
    public bool DeleteExamService(int id);
    int StudentSubmitExam(int StudentId, int examId, List<quesrtionsAnswersViewModel> quesrtionsAnswersViewModel);
}
