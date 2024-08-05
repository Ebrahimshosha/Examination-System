using ExaminationSystem.Api.DTO.Question;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.CreateQuestionViewModel;
using System.Collections.Generic;

namespace ExaminationSystem.Api.Services.QuestionService;

public interface IQuestionService
{
    Question AddQuestion(CreateQuestionDto model);
    int CalculateTotalGrade(ICollection<int> Qids);
    List<int> CreateRandomQuestionsIds(int NumberOfQuestions, int CoureId);

    List<int> CreateSpecificRandomQuestionsIds(IQueryable<Question> questions, int NumberOfQuestions);
}


