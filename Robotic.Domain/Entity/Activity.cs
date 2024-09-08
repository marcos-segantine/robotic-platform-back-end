using Robotic.Domain.Commom;

namespace Robotic.Domain.Entity;

public class Activity : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Question { get; private set; }
    public string[] Alternatives { get; private set; }
    public short Points { get; private set; }
    
    public Activity(Guid id, string name, string question, string[] alternatives, short points)
    {
        Id = id;
        Name = name;
        Question = question;
        Alternatives = alternatives;
        Points = points;
    }
}