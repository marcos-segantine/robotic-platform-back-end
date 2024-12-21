namespace Robotic.Application.DTOs;

public class ActivityDTO
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Question { get; private set; }
    public string[] Alternatives { get; private set; }
    public short Points { get; private set; }
    public string[] Resources { get; private set; }
    public string Summarize { get; private set; }
    public string Explanation { get; private set; }
    
    public ActivityDTO(
        Guid id,
        string title,
        string question,
        string[] alternatives,
        short points,
        string[] resources,
        string summarize,
        string explanation)
    {
        var year = DateTime.Today.Year;
        var month = DateTime.Today.Month;
        var day = DateTime.Today.Day;
        var hour = DateTime.Now.Hour;
        var minutes = DateTime.Now.Minute;
        var seconds = DateTime.Now.Second;
        
        Id = id;
        Title = title;
        Question = question;
        Alternatives = alternatives;
        Points = points;
        Resources = resources;
        Summarize = summarize;
        Explanation = explanation;
    }
}