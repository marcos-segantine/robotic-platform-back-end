using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

public class StudentDTO
{
    public string Name { get; private set; }
    public School School { get; private set; }
    public Schooling Schooling { get; private set; }
    public string PhotoPath { get; private set; }
    public int Points { get; private set; }
    public ScheduleClass ScheduleClass { get; private set; }
    public List<Statistics> Statistics { get; private set; }

    public StudentDTO(
        string name,
        School school,
        Schooling schooling,
        string photoPath,
        int points,
        ScheduleClass scheduleClass,
        List<Statistics> statistics)
    {
        Name = name;
        School = school;
        Schooling = schooling;
        PhotoPath = photoPath;
        Points = points;
        ScheduleClass = scheduleClass;
        Statistics = statistics;
    }
}