using AutoMapper;
using ExaminationSystem.Api.DTO.Choices;
using ExaminationSystem.Api.DTO.Question;
using ExaminationSystem.Api.DTO.Student;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.CreateQuestionViewModel;

namespace ExaminationSystem.Api.profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentToReturnDto>();
    }
}
