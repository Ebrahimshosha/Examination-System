using AutoMapper;
using ExaminationSystem.Api.DTO.Choices;
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
                opt => opt.MapFrom(src => src.Choices.Where(x => !x.IsDeleted).Select(c => c.Text).ToList())) // !
            .ForMember(des => des.RightAnswer,
                opt => opt.MapFrom(src => src.Choices.FirstOrDefault(c => c.IsRightAnswer).Text));


        CreateMap<CreateQuestionViewModel, Question>()
            .ForMember(dest => dest.Choices,
                opt => opt.MapFrom(src => src.Choices.Select(text => new Choice { Text = text }).ToList()))
            .AfterMap((src, dest) =>
                {
                    if (!string.IsNullOrEmpty(src.RightAnswer))
                    {
                        foreach (var choice in dest.Choices)
                        {
                            if (choice.Text == src.RightAnswer)
                            {
                                choice.IsRightAnswer = true;
                            }
                        }
                    }
                });

        CreateMap<ChoicesDto, Choice>();
    }
}
