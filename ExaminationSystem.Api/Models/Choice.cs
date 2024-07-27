namespace ExaminationSystem.Api.Models;

public class Choice :BaseModel
{
    public string Text { get; set; }

    public bool IsRightAnswer { get; set; }

    
    public int QuestionID { get; set; }
    public Question Question { get; set; }
}
