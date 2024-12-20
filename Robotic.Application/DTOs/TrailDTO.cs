using Robotic.Domain.Enum;

namespace Robotic.Application.DTOs;

public class TrailDTO
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Summarize { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public Guid[] Activities { get; private set; }
    public Schooling? Schooling { get; private set; }
    
    public TrailDTO(
        Guid id,
        string name,
        string summarize,
        Difficulty difficulty,
        Guid[] activities,
        Schooling? schooling)
    {
        Id = id;
        Name = name;
        Summarize = summarize;
        Difficulty = difficulty;
        Activities = activities;
        Schooling = schooling;
    }
}