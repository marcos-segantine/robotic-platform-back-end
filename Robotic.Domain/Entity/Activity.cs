using Robotic.Domain.Commom;

namespace Robotic.Domain.Entity;

public class Activity : BaseEntity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Question { get; private set; }
    public string[] Alternatives { get; private set; }
    public short Points { get; private set; }
    
    public Activity(Guid id, string title, string question, string[] alternatives, short points)
    {
        Id = id;
        Title = title;
        Question = question;
        Alternatives = alternatives;
        Points = points;
    }
}