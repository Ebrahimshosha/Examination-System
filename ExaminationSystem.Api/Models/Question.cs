namespace ExaminationSystem.Api.Models;

public class Question:BaseModel
{
    
    public string Text { get; set; }
    public int Grade { get; set; }
    public QStatus Status { get; set; }
    public ICollection<Choice> Choices { get; set; } = new HashSet<Choice>();

    public ICollection<ExamQuestion> ExamQuestions { get; set; } = new HashSet<ExamQuestion>();
}
