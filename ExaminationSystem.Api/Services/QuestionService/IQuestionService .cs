using ExaminationSystem.Api.DTO.Question;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.CreateQuestionViewModel;

namespace ExaminationSystem.Api.Services.QuestionService;

public interface IQuestionService
{
    Question AddQuestion(CreateQuestionDto model);

}
