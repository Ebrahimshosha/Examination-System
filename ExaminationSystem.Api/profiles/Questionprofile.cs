using AutoMapper;
using ExaminationSystem.Api.DTO.Question;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.CreateQuestionViewModel;

namespace ExaminationSystem.Api.profiles;

public class Questionprofile : Profile
{
    public Questionprofile()
    {
        CreateMap<CreateQuestionViewModel, CreateQuestionDto>();

        CreateMap<Question, QuestionToReturnDto>()
            .ForMember(des => des.Choices,
            opt => opt.MapFrom(src => src.Choices.Select(c => c.Text).ToList()));

  
        CreateMap<CreateQuestionViewModel, Question>()
            .ForMember(dest => dest.Choices,
                opt => opt.MapFrom(src => src.Choices.Select(text => new Choice { Text = text }).ToList()));
    }
}
