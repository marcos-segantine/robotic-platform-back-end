using Robotic.Domain.Commom;

namespace Robotic.Domain.Entity;

public class Activity : BaseEntity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Question { get; private set; }
    public string[] Alternatives { get; private set; }
    public short Points { get; private set; }
    public string[] Resources { get; private set; }
    public string Summarize { get; private set; }
    public string Explanation { get; private set; }
    
    public Activity(
        Guid id,
        string title,
        string question,
        string[] alternatives,
        short points,
        string[] resources,
        string summarize,
        string explanation)
    {
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