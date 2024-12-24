using Robotic.Domain.Commom;
using Robotic.Domain.Enum;

namespace Robotic.Domain.Entity;

public class Student : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public School School { get; private set; }
    public Schooling Schooling { get; private set; }
    public string PhotoPath { get; private set; }
    public int Points { get; private set; }
    public ScheduleClass ScheduleClass { get; private set; }
    public List<Statistics> Statistics { get; private set; }

    public Student(Guid id, string name, School school, Schooling schooling, string photoPath, int points, ScheduleClass scheduleClass, List<Statistics> statistics)
    {
        Id = id;
        Name = name;
        School = school;
        Schooling = schooling;
        PhotoPath = photoPath;
        Points = points;
        ScheduleClass = scheduleClass;
        Statistics = statistics;
    }
}

public class Certifications
{
    public IEnumerable<Guid> Done { get; private set; }
    public IEnumerable<Dictionary<Guid, int>> InProgress { get; private set; }
    public IEnumerable<Guid> NotStarted { get; private set; }

    public Certifications(IEnumerable<Guid> done, IEnumerable<Dictionary<Guid, int>> inProgress, IEnumerable<Guid> notStarted)
    {
        Done = done;
        InProgress = inProgress;
        NotStarted = notStarted;
    }
}

public class Statistics
{
    public Dictionary<Guid, int> Activity { get; private set; }

    public Statistics(Dictionary<Guid, int> activity)
    {
        Activity = activity;
    }
}