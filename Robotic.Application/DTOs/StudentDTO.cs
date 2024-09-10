using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

public class StudentDTO
{
    public string Name { get; private set; }
    public School School { get; private set; }
    public Schooling Schooling { get; private set; }
    public string PhotoPath { get; private set; }
    public int Points { get; private set; }
    public Certifications Certificates { get; private set; }
    public ScheduleClass ScheduleClass { get; private set; }

    public StudentDTO(string name, School school, Schooling schooling, string photoPath, int points, Certifications certificates, ScheduleClass scheduleClass)
    {
        Name = name;
        School = school;
        Schooling = schooling;
        PhotoPath = photoPath;
        Points = points;
        Certificates = certificates;
        ScheduleClass = scheduleClass;
    }
}