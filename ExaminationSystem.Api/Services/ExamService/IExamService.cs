using ExaminationSystem.Api.DTO.Exam;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.Exam;
using ExaminationSystem.Api.ViewModels.QuesrtionsAnswersViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Api.Services.ExamService;

public interface IExamService
{
    ExamToReturnDto AddExamService(CreateExamDto model, int totalgrade);
    // IEnumerable<Exam> GetAllExamsService();
    //Exam GetExamServiceById(int id);
    //Exam UpdateExamService(int id, ExamDTO examDTO);
    //Exam TakeExam(int studentId, int Courseid, string examStatus);
    //bool DeleteExamService(int id);
    //int StudentSubmitExam(int StudentId, int examId, List<quesrtionsAnswersViewModel> quesrtionsAnswersViewModel);
}
