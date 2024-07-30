namespace ExaminationSystem.Api.Services.ChoiceService;

public interface IChoiceService
{
    void Addchoice(int QuestionId, List<string> choices, string RightAnswer);
    void Removechoice(int QuestionId);

}
