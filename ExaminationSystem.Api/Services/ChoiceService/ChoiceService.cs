
namespace ExaminationSystem.Api.Services.ChoiceService;

public class ChoiceService: IChoiceService
{
    private readonly IGenericRepository<Choice> _choiceRepository;

    public ChoiceService(IGenericRepository<Choice> Choicerepository)
    {
        _choiceRepository = Choicerepository;
    }
    public void Addchoice(int QuestionId, List<string> choices, string RightAnswer)
    {
        foreach (var chioce in choices)
        {
            _choiceRepository.Add(new Choice
            {
                Text = chioce,
                QuestionID = QuestionId,
                IsRightAnswer = string.Equals(chioce.ToLower(), RightAnswer.ToLower())

            });
        }

        _choiceRepository.SaveChanges();
    }

    public void Removechoice(int QuestionId)
    {
       var choices =  _choiceRepository.GetAll().Where(x => x.QuestionID == QuestionId);
        foreach (var choice in choices)
        {
            _choiceRepository.Delete(choice);
        }
        _choiceRepository.SaveChanges();

    }
    public IQueryable<Choice> GetChoicesbyQid(int id)
    {
    var choices = _choiceRepository.Get(c => c.QuestionID == id);

        return choices;
    }
}
