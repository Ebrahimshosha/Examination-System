
namespace ExaminationSystem.Api.profiles;

public class ExamProfile:Profile
{
    public ExamProfile()
    {
        CreateMap<Exam,ExamToReturnDto>()
            .ForMember(des=>des.ExamStatus,opt=>opt.MapFrom(src=>src.ExamStatus.ToString()));

        CreateMap<CreateManualExamViewModel, ExamManualDto>();
        CreateMap<CreateAutomaticExamViewModel, ExamAutomaticDto>();
        CreateMap<CreateExamViewModel, Exam>();
        CreateMap<CreateExamViewModel, ExamDTO>();
    }
}
