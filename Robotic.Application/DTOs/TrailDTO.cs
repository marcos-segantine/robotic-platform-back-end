using Robotic.Domain.Enum;

namespace Robotic.Application.DTOs;

public class TrailDTO
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Resume { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public Guid[] Activities { get; private set; }
    public Schooling? Schooling { get; private set; }
    
    public TrailDTO(Guid id, string name, string resume, Difficulty difficulty, Guid[] activities, Schooling? schooling)
    {
        Id = id;
        Name = name;
        Resume = resume;
        Difficulty = difficulty;
        Activities = activities;
        Schooling = schooling;
    }
}