using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.Exam;

namespace ExaminationSystem.Api.Services.ExamService;

public interface IExamService
{
    Exam CreateExamService(CreateExamViewModel viewModel);
}
