using AutoMapper;
using ExaminationSystem.Api.DTO.Exam;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.Exam;

namespace ExaminationSystem.Api.profiles;

public class ExamProfile:Profile
{
    public ExamProfile()
    {
        CreateMap<Exam,ExamToReturnDto>()
            .ForMember(des=>des.ExamStatus,opt=>opt.MapFrom(src=>src.ExamStatus.ToString()));

        CreateMap<CreateExamViewModel, ExamDto>();
    }
}
