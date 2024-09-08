namespace Robotic.Application.DTOs;

public class ActivityDTO
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Resume { get; private set; }
    public string Question { get; private set; }
    public string[] Alternatives { get; private set; }
    public short Points { get; private set; }
    
    public ActivityDTO(Guid id, string name, string resume, string question, string[] alternatives, short points)
    {
        var year = DateTime.Today.Year;
        var month = DateTime.Today.Month;
        var day = DateTime.Today.Day;
        var hour = DateTime.Now.Hour;
        var minutes = DateTime.Now.Minute;
        var seconds = DateTime.Now.Second;
        
        Id = id;
        Name = name;
        Resume = resume;
        Question = question;
        Alternatives = alternatives;
        Points = points;
    }
}