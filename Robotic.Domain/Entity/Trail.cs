using Robotic.Domain.Enum;

namespace Robotic.Domain.Entity;

public class Trail
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Resume { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public Guid[] Activities { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }
    public Schooling? Schooling { get; private set; }
    
    public Trail(Guid id, string name, string resume, Difficulty difficulty, Guid[] activities, Schooling? schooling)
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
        Difficulty = difficulty;
        Activities = activities;
        Schooling = schooling;
        
        CreatedOn = new DateTime(year, month, day, hour, minutes, seconds).ToUniversalTime();
        ModifiedOn = new DateTime(year, month, day, hour, minutes, seconds).ToUniversalTime();
    }
}